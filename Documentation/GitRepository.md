[<BACK](README.md)

# GIT Repository

## Folder Structure

This repo uses the following folder structure:  

```
|--Documents/
|--System/
|----Development
|------Source
|------Spikes
|------Tools
|--Operations
|----Build
|----Deploy
|----Provision

```
### Document

Contains documentation about the system including data flow, physical design, and logical design.  

### System  
Separates the core system from the documents folder.  Builds can be bound to this folder to prevent it from being triggered when documents are updated.

#### Development
Contains everything that is necessary to build the entire solution.

##### Source
Supports multiple solutions in this repository.  

* Each solution should be in its own folder with same name
* Each project should be in its own sub-folder

#####  Spikes
Items that are not true project artifacts. I.e. Proof of concepts.

#####  Tools
Pre-compiled executables and utilities separated by folders for each.

### Operations
Used by the build process to provision, build, and deploy the system.

## Branching Strategy 
This Repo follows the GitHub Flow branching strategy which utilizes a single continuously deployable main branch.  Feature branches are used to build features or bug fixes.  Each must be finished and completely tested before merged to main.

## Commits
All comments must be associated with at least one issue.  

[<BACK](README.md)
