[<AutoOpen>]
module Node.Api

open System
open Fable.Core
open Fable.Core.JS
open Node
open Node.Base

let [<Global>] __filename: string = jsNative
let [<Global>] __dirname: string = jsNative
let [<Global>] ``global``: Global = jsNative
let [<Global>] require: NodeRequire = jsNative
let [<Global>] ``module``: NodeModule = jsNative
let [<Global>] exports: obj = jsNative
let [<Global>] ``process``: Process.Process = jsNative
//let [<Global>] performance: Performance.Performance = jsNative
let [<Global>] URLSearchParams: Url.URLSearchParams = jsNative

[<Obsolete("USE JS.console from Fable.Core instead")>]
let console : unit = jsNative 

[<Import("*", "buffer")>]
let buffer: Buffer.IExports = jsNative

[<Import("*", "child_process")>]
let childProcess: ChildProcess.IExports = jsNative

[<ImportDefault("cluster")>] // avoid problems with babel
let cluster: Cluster.IExports = jsNative

[<Import("*", "events")>]
let events: Events.IExports = jsNative

[<Import("*", "fs")>]
let fs: Fs.IExports = jsNative

[<Import("*","net")>]
let net: Net.IExports = jsNative

[<Import("*","crypto")>]
let crypto: Crypto.IExports = jsNative

[<Import("*","tls")>]
let tls: Tls.IExports = jsNative

[<Import("*","http")>]
let http: Http.IExports = jsNative

[<Import("*","https")>]
let https: Https.IExports = jsNative

[<Import("*", "os")>]
let os: OS.IExports = jsNative

[<Import("performance", "perf_hooks")>]
let performance: Performance.Performance = jsNative

[<Import("PerformanceObserver", "perf_hooks")>]
let PerformanceObserver: Performance.PerformanceObserver = jsNative

[<Import("*", "querystring")>]
let querystring: Querystring.IExports = jsNative

[<Import("*", "stream")>]
let stream: Stream.IExports = jsNative

[<Import("*", "url")>]
let URL: Url.URLType = jsNative

[<Import("*", "trace_events")>]
let TraceEVents: TraceEvents.IExports = jsNative

//[<Import("*", "util")>]
//let Util: Util.IExports = jsNative

[<Import("*", "path")>]
let path: Path.IExports = jsNative

[<Import("*", "dgram")>]
let dgram: Dgram.IExports = jsNative

[<Import("*", "worker_threads")>]
let workerThreds: WorkerThreads.IExports = jsNative
