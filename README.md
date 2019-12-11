# Entegy Candidate Application

## Design Choice Assumptions
### Number to Words Logic
I initially implemented a solution utilising a number of ternary operators based off the following stackoverflow post: https://stackoverflow.com/a/1691675/522859. I found the concise structure of the code made it significantly easier to read when compared to most other solutions.

![](https://lh6.googleusercontent.com/vZ12GLqrMh-0q5ivZKzwBbIKIVrPRifRSpjR3rqYhkCuWvPikhAX5pIn0vW-LDqFTvDmH2i5bMQ23L9ysxs8jzv8UDUOzOaA39sFLI62UUYlLhGUvcXFseKnlOOJP89hC-NHcFtm)

After a little testing I found that while the ternary operators appeared neater they made debugging tedious. It was difficult to determine which operator was causing an issue without manually stepping through the code. After refactoring I converted the solution into something similar to the following: https://stackoverflow.com/a/794831/522859

![https://stackoverflow.com/a/794831/522859](https://lh4.googleusercontent.com/bkG9LXIo9MaYn57AzhWJEBQi5PdlQon3EJLPkDAcB5jdY5N4ydhzHZ8sUNW_29-2v-QidPjbrOa5_YqYQL6sHtgQCY8yq-ADa2BB5SIZST-eVP9SXeXPFXNvEZRa4ftdSnOAwAtE)

An alternative to rolling my own custom convertor would’ve been to utilise an existing library. This may have been beneficial as it would have addressed edge cases that I only found through testing. A potential candidate may have been  https://humanizr.net/.

### Number Concatenation

It was assumed that “AND” should not be included when concatenating numbers due to the following example in the project description:

> ONE THOUSAND TWO HUNDRED THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS 

If the solution were modified to include “AND” as a concatenator the output would have been as follows:

> ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS

### Zero cents
It was assumed that when there is a dollar amount with no cents value that the cheque writer should ouput the words "ZERO CENTS".  
 
## How to Use the Service
The service offers a standard HTTP endpoint that can be accessed locally on port 44393. The amountToWords service is accessible via a GET request at the following url:

[https://localhost:44394/cheques/amountInWords](https://localhost:44394/cheques/amountInWords)

The following examples show how the service can be used. The sample requests can be added to a tool such as Fiddler or Postman.

The payload below requests that the amountInWords service converts the decimal amount of 49.95 to words

**Valid Request**  
`GET https://localhost:44394/cheques/amountInWords?amount=49.95 HTTP/1.1`  
`Host: localhost:44394`  
`Content-Length: 0`  
`Content-Type: application/json`  


**Expected Response**  
`HTTP/1.1 200 OK`  
`Content-Type: application/json; charset=utf-8`  
`Server: Microsoft-IIS/10.0`  
`X-Powered-By: ASP.NET`  
`Date: Wed, 11 Dec 2019 04:17:03 GMT`  
`Content-Length: 123`  
` `  
`{"success":true,"messages":[],"data":{"amountInWords":"FORTY-NINE DOLLARS AND NINETY-FIVE CENTS"},"page":0,"maxPageSize":0}`  

The next payload provides an example of what to expect when an error occurs. When an invalid amount is passed to the service it will return an error message. The success flag will also be false:

**Invalid Request**

`GET https://localhost:44394/cheques/amountInWords?amount=INVALID_AMOUNT HTTP/1.1`  
`Host: localhost:44394`  
`Content-Length: 0`  
`Content-Type: application/json`  

**Expected Response**

`HTTP/1.1 200 OK`  
`Content-Type: application/json; charset=utf-8`  
`Server: Microsoft-IIS/10.0`  
`X-Powered-By: ASP.NET`  
`Date: Wed, 11 Dec 2019 04:17:59 GMT`  
`Content-Length: 159`  

`{"success":false,"messages":[{"id":null,"text":"Please ensure that amount is a valid decimal: $INVALID_AMOUNT","type":0}],"data":null,"page":0,"maxPageSize":0}`

## Testing and Validating the Service
CandidateAssessmentTestProject has been included in this solution to run integration tests. These tests attempt to verify that the controller and service return an expected response for each of the tested inputs.

To ease the testing process, an in memory database has also been setup for creating and storing cheques. This will be cleared each time the web server is restarted.

In addition to these automated tests this document includes a number of raw requests that can be used in tools such as Fiddler and Postman.

## Potential Future Enhancements

### Globalization
Ideally, globalization/localization would be supported. A quick Google suggests something like the following would likely work: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-3.1

One potential caveat is the fact that not all languages format money or numbers in the same way. This means that a different “NumberToWords” function may be required for some localizations.

### Authentication/Authorization
If these are intended to be valid cheques authentication and authorization would be mandatory. I’ve recently been using AWS Cognito. This would also allow for requestor details to be stored against each cheque that is generated.

### Swagger
If additional services were to be added to this API it would likely merit the inclusion of a documentation generation tool such as Swagger. These tools provide a familiar interface to developers and can significantly decrease the amount of documentation required.

### Logging
A proper logging implementation would be required if the service were to be used in earnest. RayGun would potentially serve as a suitable candidate.
