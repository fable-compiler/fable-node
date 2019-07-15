module Cluster

open Fable.Core
open Fable.Core.DynamicExtensions
open Node.Base
open Node.Http
open Node.Cluster

let cluster = Node.Api.cluster
let http = Node.Api.http
let numCPUS = Node.Api.os.cpus() |> Seq.length
let nprocess = Node.Api.``process``
let pid = nprocess.pid 

if cluster.isMaster then 

  JS.console.log (sprintf "Master %.0f is running" pid)
  
  // creating workers
  [1..numCPUS] |> Seq.iter( fun _ -> cluster.fork() |> ignore )
  
  cluster.on("exit", fun (worker:Worker) code signal -> 
    let id = worker.``process``.pid
    JS.console.log(sprintf "worker %.0f died" id)
  ) |> ignore

else
  
  // our http servers
  let listener (req:IncomingMessage) (res:ServerResponse) = 
    res.writeHead 200
    res.``end`` "Fable on a Node.js Cluster!!"

  http.createServer( listener).listen 8000 |> ignore
  JS.console.log( sprintf "worker %.0f started" pid )
