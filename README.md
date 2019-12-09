# fable-node

[![Nuget Package](https://img.shields.io/nuget/v/Fable.Node.svg)](https://www.nuget.org/packages/Fable.Node)

Fable bindings for [node.js native modules](https://nodejs.org/api/)

**Currently supporting Node.JS 10.x LTS**

## Installing

To install in your project reference the `Fable.Node` NuGet package

If you use Paket:
```bash
paket add Fable.Node
```

If you use NuGet `<PackageReference />`:
```bash
dotnet add package Fable.Node
```

## Development

- Tests: `npm test` 
- Publish: `npm run build publish` 

## Ongoing process

While most of the previous Node.js API has already been mapped to Fable bindings some time ago, we've decided to make sure it will be compatible with current Node.js LTS (10.x).

Actually here's the actual progresses made on the updates:

## Updated for Node.js 10.x LTS

- [x] Buffer *(from 1.0.0-beta-0.0.1)*
- [x] OS *(from 1.0.0-beta-0.0.1)*
- [x] Path *(from 1.0.0-beta-0.0.1)*
- [x] Url *(from 1.0.0-beta-0.0.1)*
- [x] Performance Hooks *(from v1.0.0)*
- [x] Cluster *(from v1.0.1)*
- [x] Trace Events *(from v1.0.1)*

## Not yet updated

*No check can either mean it doesn't exist*

- [X] Child Processes
- [ ] Crypto
- [ ] DNS
- [ ] Events
- [ ] File System
- [X] HTTP
- [ ] HTTP/2
- [X] HTTPS
- [ ] Inspector
- [ ] Net
- [X] Process
- [X] Query Strings
- [ ] Readline
- [ ] REPL
- [ ] Report
- [X] Stream
- [ ] String Decoder
- [ ] Timers
- [X] TLS/SSL
- [X] Trace Events
- [ ] TTY
- [ ] UDP/Datagram
- [X] URL
- [ ] Utilities
- [ ] V8
- [ ] VM
- [ ] Worker Threads 
- [ ] Zlib
