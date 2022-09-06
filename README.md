# Using a BackgroundWorker and progress bar in XAF for Windows

![Demo](https://github.com/egarim/XafWinBackgroundWorker/blob/master/XafBackgroundWorker.gif?raw=true)

### The problem
To process data in the background show the user a represantation of the progress while keeping the application responsive

### The solution
Use a [background worker](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?view=net-6.0) to process data 
on the background, report the progress to the main thread where the object space of the controller is available and show the user the state of the process


### Important files

- [The progress bar property editor](https://github.com/egarim/XafWinBackgroundWorker/blob/master/XafWinBackgroundWorker.Module.Win/Editors/ProgressBarPropertyEditor.cs) used to display the process porgress 

- The [view controller](https://github.com/egarim/XafWinBackgroundWorker/blob/master/XafWinBackgroundWorker.Module/Controllers/DirectoryController.cs) what handles the backgrown process
