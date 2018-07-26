# BlazorWebWinClient
Basic Blazor function and usage of reflection to execute .NET Assembly functions in the Browser

You can try out the [sample Website](https://chtau.github.io/BlazorWeb/)

This is mostly used to understand and Test the functions of the Blazor Framework.

* Blazor Markup
* Access Local Storage
* Javescript Interop
* Assembly Reflection


## Getting Started

Import an exsting .NET Assembly File (currently only Tested with Framework Version 4.7). 
The Platform Target for the DLL must be x64, x86 or Any Platform would give you a Bad Image Format exception.

![Import Assembly in Blazor](https://raw.githubusercontent.com/Chtau/BlazorWebWinClient/master/docs/img/BlazorWebWinClient_Import_DLL.PNG)

Imported Assemblies can be save in the local Storage. If the local Storage already contains any stored Assemblies they will be automatic loaded.
(You can download DLL File for Testing from the samples Folder)

In the Terminal you can use "?" or "help" to see all Commands.

![Terminal in Blazor website](https://raw.githubusercontent.com/Chtau/BlazorWebWinClient/master/docs/img/BlazorWebWinClient_Terminal.PNG)

# About

~~Current Version 0.1.0
Build on the [Blazor](https://github.com/aspnet/Blazor) Version 0.4.0~~

Version 0.2.0
Update to [Blazor](https://github.com/aspnet/Blazor) Version 0.5.0
