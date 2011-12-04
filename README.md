What is it?
-----------

MSBus (for Mirco Service Bus) is a lightweight and high throughput (hopefully) message transport system for Windows. It's not exactly a message queuing system such as RabbitMQ or ZeorMQ, though it does draw inspiration from them. Read below for more on just how it works.

It runs as either a console application for easy testing and development, or as a Windows Service (*coming soon!*). The only real requirements are Windows, and .NET 4.0 - everything else is bundled in the application.

Getting Started
---------------

The easiest way to get started is to just run the `msbuscl.exe` application. That's it, you're done.

How do I use it?
----------------

MSBus exposes a rather simple RESTful interface that can be easily called from most languages and platforms. We will soon be adding a C# client that will make using MSBus from a .NET application even easier.

For information on writing your own client, look at our [API Specification](https://github.com/adamcaudill/msbus/wiki/API-Specification)

Technology
----------

As is the case with many such projects, we rely on a number of other open-source projects. Here's a rough list (sorry if I missed any!):

 - [Kayak](https://github.com/kayak/kayak)
 - [NancyFx](http://nancyfx.org/)
 - [Gate](https://github.com/owin/gate)
 - [Json.NET](http://james.newtonking.com/projects/json-net.aspx)
 - [RavenDB](http://www.ravendb.net/)

Status
------

What's the status of the project?

In a word: Young.

This is still a fairly new project with a lot of work to be done, but progressing rapidly. From a feature perspective, everything works. Would I use it in production? Not *yet*, but very soon. I expect the first stable release to be ready for prime time by **Jan 1st, 2012**.

Issues & TODOs
--------------

For information about what's missing, what's broken, and what just plain wrong, take a look at the [Issues](https://github.com/adamcaudill/msbus/issues) page.