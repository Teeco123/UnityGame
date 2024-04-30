# Versions

Unity - **2022.3.23f1**

# C# code Style Guide
**Everyone Should use Styling Guide so our code is more readable and easier to maintain**

## Code
  + Names of classes, methods, enums, namespaces: `PascalCase`
  + Names of local variables, parameters: `camelCase`
  + Names of interfaces starts with `I` e.g.  `IInterface`
## Files
  + Filenames and directory names: `PascalCase`
  + Where possible file name should be the same as the name of main class e.g. `MyClass.cs`
## Organization
  + Namespace `using` declarations go at the top before any namespaces/
  + Each variables group should be in following order
    - Public
    - Protected
    - Private

# Unity Smart Merge Tool

We gonna use Unity Smart Merge for scene and prebafs conflicts that we would have to resolve manually.

## Smart Merge Tool setup

**Find path to your Merge Tool.** Unity says that default path should be: <br/>
   + _Windows_ <br/>
`C:\Program Files\Unity\Editor\Data\Tools\UnityYAMLMerge.exe` <br/>
or <br/>
`C:\Program Files (x86)\Unity\Editor\Data\Tools\UnityYAMLMerge.exe` <br/>
   + _MacOS_ <br/>
`/Applications/Unity/Unity.app/Contents/Tools/UnityYAMLMerge` <br/>

  But I actually found it on windows in: <br/>
`C:\Program Files\Unity\Hub\Editor\2022.3.23f1\Editor\Data\Tools\UnityYAMLMerge.exe` <br/>
  Use what works for you.

## Git setup
1. Make sure u have visible hidden folders and files and open `.git` folder and then edit `config` file.
2. In `config` file add following lines.
```
[merge]
 	tool = unityyamlmerge
[mergetool "unityyamlmerge"]
 	trustExitCode = false
 	cmd = '<Path to your Merge Tool>' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
 ```
## How to use 
Whenever you're merging or rebasing your project and a conflict appears, instead of manually fixing it you open **Git Shell/Bash** and type the following command: **`git mergetool`**.
The tool will then resolve those conflicts for you automatically. </br>

**You will need then to run the command `git add --all` or `git add .` in order to save the changes made, and then `git rebase --continue` when rebasing or `git merge --continue` when merging.**
