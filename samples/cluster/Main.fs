module Cluster

open Fable.Core
open Fable.Core.DynamicExtensions
open Node.Base
open Node.Http
open Node.Cluster

let cluster = Node.Api.cluster
let pid = Node.Api.``process``.pid

if cluster.isMaster then 

  JS.console.log (sprintf "Master %.0f is running" pid)
  
  // creating workers
  let numCPUS = Node.Api.os.cpus() |> Seq.length
  [1..numCPUS] |> Seq.iter( fun _ -> cluster.fork() |> ignore )

  let onExit (worker:Worker) code signal =
    let id = worker.``process``.pid
    JS.console.log(sprintf "worker %.0f died" id)

  cluster.on("exit", onExit) |> ignore

else
  
  // our http servers
  let http = Node.Api.http
  let listener (req:IncomingMessage) (res:ServerResponse) = 
    res.writeHead 200
    res.``end`` "Fable on a Node.js Cluster!!"

  http.createServer(listener).listen 8000 |> ignore
  JS.console.log( sprintf "worker %.0f started" pid )
