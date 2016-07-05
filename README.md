#Face API - Verify
Microsoft has recently released a set of Cognitive Services that make it easy to add powerful machine intelligence to your applications with just a few lines of code. Since these services are hosted in the cloud, they can easily be consumed by applications running on any platform. 

This example demonstrates the Face API, one of the Cognitive Services available from Microsoft. You can read my article on the Face API at [http://jamesmccord.azurewebsites.net/](http://jamesmccord.azurewebsites.net/).

##Face API Features
The Face API provides several areas of functionality that I’ll review independently. These are:

**Face Detection**

Face Detection is the process of detecting faces in an image and returning the location of the faces in the image. This feature can also estimate face attributes including age and gender.

**Face Verification (covered in this example)**

Face Verification takes two face images and determines the likelihood that these faces belong to the same person.

**Face Identification**

Face Identification allows you to create face groups containing persons, where each person is defined by one or more images. You can then determine whether a new image belongs to any of the persons in the face group. 

**Similar Face Searching**

Similar Face Searching will identify similar faces from a list of faces matching a specific face. 

**Face Grouping**

Face Grouping organizes a group of faces based on similar facial characteristics. It returns a list of grouped faces and identifies any faces that weren’t grouped.


