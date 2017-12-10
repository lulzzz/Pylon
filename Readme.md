# Aiursoft.Pylon

[![Build Status](https://travis-ci.org/AiursoftWeb/Pylon.svg?branch=master)](https://travis-ci.org/AiursoftWeb/Pylon)

Current version: `0.4.0`

## What is Pylon

The Pylon is the Protoss supply building and the primary source of the Psionic Matrix power field. After warping in, the Pylon is surrounded by a circular field of the Psionic Matrix, providing power to nearby structures and enabling units to warp in on demand from Warp Gates within a radius of 6.5. Each Pylon also provides 8 Psi supply points.

## Where this project runs

Pylon is the place where shared code located for Aiursoft web apps.

These shared code contains base classes, interfaces and services.

## How to install Pylon to your project

Excute:

`> Install-Package Aiursoft.Pylon -Version 0.1.0`

Or:

`> dotnet add package Aiursoft.Pylon --version 0.1.0`


## How to publish

Please excuse the following commands in the project folder:

    dotnet restore
    dotnet pack

## What is the relationship with other Aiursoft apps

To produce strong-type API access between Aiursoft services, Aiursoft.Pylon produce all method for all resetful-APIs.

**Pylon did not implement any API!**

## How to contribute

There are many ways to contribute to the project: logging bugs, submitting pull requests, reporting issues, and creating suggestions.

Even if you have push rights on the repository, you should create a personal fork and create feature branches there when you need them. This keeps the main repository clean and your personal workflow cruft out of sight.

We're also interested in your feedback for the future of this project. You can submit a suggestion or feature request through the issue tracker. To make this process more effective, we're asking that these include more information to help define them more clearly.