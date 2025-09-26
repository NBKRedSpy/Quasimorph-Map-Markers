This project uses a monolithic git repository and assumes the following folders will be available:

|Folder|Description|
|--|--|
|main-repo|The git repository and the 'main' (stable) branch.
|beta|The 'beta' branch worktree|
|bootstrap|The 'bootstrap' branch worktree|

To build, use the bootstrap/build.ps1 script.  This will automatically build all of the projects, deploy to the Steam Workshop folder (if the Steam ID is set), and create the release zip file in the bootstrap folder.

