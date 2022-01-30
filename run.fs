open System.IO

open RunHelpers
open RunHelpers.Templates

[<RequireQualifiedAccess>]
module Config =
    let lib = "src/AnalogGameEngine/SimpleGUI/AnalogGameEngine.SimpleGUI.fsproj"
    let prototypes = "./src/prototypes"

module Task =
    let restoreLib () = DotNet.restoreWithTools Config.lib
    let buildLib () = DotNet.build Config.lib Debug

    let restoreAll () =
        job {
            DotNet.toolRestore ()
            DotNet.restore Config.lib

            for prototype in Directory.EnumerateFiles(Config.prototypes, "*.fsproj", SearchOption.AllDirectories) do
                DotNet.restore prototype
        }

    let buildAll () =
        job {
            DotNet.build Config.lib Debug

            for prototype in Directory.EnumerateFiles(Config.prototypes, "*.fsproj", SearchOption.AllDirectories) do
                DotNet.build prototype Debug
        }

[<EntryPoint>]
let main args =
    args
    |> List.ofArray
    |> function
        | [ "restore" ] -> Task.restoreLib ()
        | [ "build" ] ->
            job {
                Task.restoreLib ()
                Task.buildLib ()
            }
        | [ "restore-all" ] -> Task.restoreAll ()
        | []
        | [ "build-all" ] ->
            job {
                Task.restoreAll ()
                Task.buildAll ()
            }
        | _ ->
            let msg =
                [
                    "Usage: dotnet run [<command>]"
                    "Look up available commands in run.fs"
                ]

            Error(1, msg)
    |> ProcessResult.wrapUp
