#r "nuget: Fable.PublishUtils, 2.4.0"

open System
open PublishUtils

// run "npm test"

let args =
    fsi.CommandLineArgs
    |> Array.skip 1
    |> List.ofArray

match args with
| IgnoreCase "publish"::_ ->
    pushNuget "src/Fable.Node.fsproj" [] doNothing
| _ -> ()
 