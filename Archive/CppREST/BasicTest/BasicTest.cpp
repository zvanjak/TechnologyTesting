// BasicTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <cpprest/http_client.h>
#include <cpprest/filestream.h>
#include <iostream>
#include <sstream>

using namespace web;                        // Common features like URIs.
using namespace web::http;                  // Common HTTP functionality
using namespace web::http::client;          // HTTP client features
using namespace concurrency::streams;       // Asynchronous streams

pplx::task<void> HTTPStreamingAsync()
{
	http_client client(L"http://www.bing.com");

	// Make the request and asynchronously process the response. 
	return client.request(methods::GET).then([](http_response response)
	{
		// Print the status code.
		std::wostringstream ss;
		ss << L"Server returned returned status code " << response.status_code() << L'.' << std::endl;
		std::wcout << ss.str();

		// TODO: Perform actions here reading from the response stream.
		auto bodyStream = response.body();

		// In this example, we print the length of the response to the console.
		ss.str(std::wstring());
		ss << L"Content length is " << response.headers().content_length() << L" bytes." << std::endl;
		std::wcout << ss.str();

		std::wcout << bodyStream;
	});

	/* Sample output:
	Server returned returned status code 200.
	Content length is 63803 bytes.
	*/
}

int executeSearch()
{
	auto fileStream = std::make_shared<std::ostream>();

	// Open stream to output file.
	pplx::task<void> requestTask = std::fstream::open_ostream(U("results.html")).then([=](std::ostream outFile)
	{
		*fileStream = outFile;

		// Create http_client to send the request.
		http_client client(U("http://www.bing.com/"));

		// Build request URI and start the request.
		uri_builder builder(U("/search"));
		builder.append_query(U("q"), U("Casablanca CodePlex"));
		return client.request(methods::GET, builder.to_string());
	})

		// Handle response headers arriving.
		.then([=](http_response response)
	{
		printf("Received response status code:%u\n", response.status_code());

		// Write response body into the file.
		return response.body().read_to_end(fileStream->streambuf());
	})

		// Close the file stream.
		.then([=](size_t)
	{
		return fileStream->close();
	});

	// Wait for all the outstanding I/O to complete and handle any exceptions
	try
	{
		requestTask.wait();
	}
	catch (const std::exception &e)
	{
		printf("Error exception:%s\n", e.what());
	}

	return 0;
}

int main(int argc, char* argv[])
{
	std::wcout << L"Calling HTTPStreamingAsync..." << std::endl;
	HTTPStreamingAsync().wait();

}
