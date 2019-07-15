module Cluster

open Fable.Core
open Fable.Core.DynamicExtensions
open Node.Base
open Node.Http
open Node.Cluster

let cluster = Node.Api.cluster
let mutable count = 0

if cluster.isMaster then 

  let pid = Node.Api.``process``.pid
  JS.console.log (sprintf "Master %.0f is running" pid)

  let spawnWorker _ = 
    let worker = cluster.fork() 
    let id = worker.``process``.pid
    JS.console.log(sprintf "SPAWNED: %.0f " id)

  // spawning one worker per CPU
  Node.Api.os.cpus() |> Seq.iter spawnWorker
  
  // if a worker dies, respawn a new one
  let onExit (deadWorker:Worker) code signal =
    let id = deadWorker.``process``.pid
    JS.console.log(sprintf "DEAD: %.0f " id)
    spawnWorker()

  cluster.on("exit", onExit) |> ignore

else
  
  // our http servers
  let http = Node.Api.http
  let listener (req:IncomingMessage) (res:ServerResponse) = 
    if count < 3 then 
      res.writeHead 200
      res.``end`` "Fable on a Node.js Cluster!!"
      count <- count + 1
    else 
      count <- 0
      raise(System.Exception"Bye bye!")

  http.createServer(listener).listen 8000 |> ignore
