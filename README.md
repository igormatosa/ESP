# Introduction 
This project contains all source code needed to develope, build en distribute CISV.
Documentation can be found on SharePoint.

# Getting Started
Keep all projects as seperated as possible
## Micro services
This project will use kubernetes as orchestrator for linux based docker images.
before starting programming
1.	Install docker for windows using linux containers
2.	Enable kubernetes (single cluster)
3.	Before uploading code, run unit tests, build and deploy container on local kubernetes first.
4.	Update any relevant documentation

## Web sites
This project will use devexpress mvc core for user controls
1. Install devexpress extension for visual studio 2019
2. Use asp.net core 3.1 as a start template with docker support
3. keep javascript and style in sepperate files
4. use the standard controls as much as possible

## Mobile apps
This project will use Xamarin forms for android
1. Use the standard libraries as much as possible

## Windows apps
This project will use mvc core 3.1 with prism as project template
1. Install prism extension for visual studio 2019
2. Use the standard libraries as much as possible

# Build and Test
- Use unit testing as much as possible.
- all unittest must be successfull before building a distributable container.
- use major.minor.patch as version number for every distributable contaiber. 
  additional a "-rc" can be added to the patch when ready for testing but not for prodcution.
- use yaml files for build pipelines

# Contribute
- when in doubt ask someone else
- Inline code comment smust be in English
- Never merge code to master or release branch that can't be build. 
 ```
  the person who is responsible for breaking a build will buy the whole team pizza and beer.
 ```
- All documentation needed for development can be found on SharePoint and must be in English
- when a different coding language then c#, javascript or typescript is needed. discus first with the whole team before using.
- A Pull request must have at least 1 reviewer who did not write the code and 1 requirement or bug linked to it
- Source code must be placed inside de src folder
- requirement or bug fix branches must start with the work item id. followed with a description eg.: [id]_[description]