module Tests.Buffer

open System
open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.Core.DynamicExtensions
open Node.OS
open Util

let buffer = Node.Api.buffer
let Buffer = Node.Api.buffer.Buffer
type Encoding = Node.Buffer.BufferEncoding

let tests : Test = 
  testList "Buffer" [
  
    testList "Buffer.constants" [
      testCase "MAX_LENGTH" <| fun _ ->
        printfn "%i" buffer.constants.MAX_LENGTH
        testPassed()

      testCase "MAX_STRING_LENGTH" <| fun _ ->
        printfn "%i" buffer.constants.MAX_STRING_LENGTH
        testPassed()
    ]

    testList "buffer.alloc" [
      testCase "do it" <| fun _ -> 
        let buf = Buffer.alloc 5
        buf.length = 5 |> equal true

      testCase "do it" <| fun _ -> 
        let buf = Buffer.alloc(5,"a")
        buf.length = 5 |> equal true

      testCase "do it" <| fun _ -> 
        let buf = Buffer.alloc(11,"aGVsbG8gd29ybGQ", Encoding.Base64)
        buf.length = 11 |> equal true
    ]

    testList "buffer.allocUnsafe" [
      testCase "allocUnsafe" <| fun _ -> 
        let buf = Buffer.allocUnsafe 5
        buf.length = 5 |> equal true      
    ]

    testList "Buffer.byteLength" [
      testCase "do it" <| fun _ -> 
        let str = "\u00bd + \u00bc = \u00be"
        let bl = Buffer.byteLength(str, Encoding.Utf8)
        printfn "%s: %i characters, %i bytes" str str.Length bl
        bl = 12 |> equal true
    ]

    testList "Buffer.compare" [
      testCase "do it" <| fun _ -> 
        let buf1 = Buffer.from("1234")
        let buf2 = Buffer.from("0123")
        Buffer.compare(buf1,buf2) = 1 |> equal true
    ]

    testList "Buffer.concat" [
      testCase "do it" <| fun _ -> 
        let buf1 = Buffer.alloc(10)
        let buf2 = Buffer.alloc(14)
        let buf3 = Buffer.alloc(18)
        let totalLength = buf1.length + buf2.length + buf3.length
        let bufA = Buffer.concat([|buf1;buf2;buf3|], totalLength)
        totalLength = bufA.length |> equal true
    ]

    testList "Buffer.fron" [
      testCase "do it" <| fun _ -> 
        let buf = Buffer.from([|0x62; 0x75; 0x66; 0x66; 0x65; 0x72|])
        buf.length = 6 |> equal true

      testCase "do it" <| fun _ -> 
        let arr = [|5000;4000|]
        let buf = Buffer.from arr.buffer
        arr.[1] <- 6000
        testPassed()

      testCase "do it" <| fun _ -> 
        let ab = ArrayBuffer.Create(10)
        let buf = Buffer.from(ab, 0, 2)
        buf.length = 2 |> equal true
      
      testCase "do it" <| fun _ -> 
        // basically a Buffer is an UInt8/byte array
        let buf1 : byte [] = unbox (Buffer.from "buffer")
        let buf2 = Buffer.from buf1

        buf1.[0] <- !!0x61
        ((string buf1) = "auffer" 
          && buf2.toString() = "buffer") |> equal true
    
      testCase "do it" <| fun _ -> 
        let buf1 = Buffer.from "this is a tést"
        let buf2 = Buffer.from("7468697320697320612074c3a97374", Encoding.Hex)
        ( 
          buf1.toString() = buf2.toString() 
          && buf1.toString(Encoding.Ascii)  =  "this is a tC)st"
          ) |> equal true
    ]    

    testList "Buffer misc" [
      testCase "poolSize" <| fun _ ->
        printfn "%i" Buffer.poolSize
        testPassed()
    ]

    testList "buf" [
      testCase "poolSize" <| fun _ ->
        printfn "%i" Buffer.poolSize
        testPassed()

      testCase "buf[index]" <| fun _ -> 
        let str = "Node.js"
        let buf : byte []= unbox (Buffer.allocUnsafe str.Length)
        for i in 0..(str.Length - 1) do
          buf.[i] <- byte (str.Chars i)

        (string buf) = str |> equal true
      
      testCase "buf.buffer" <| fun _ -> 
        let arrayBuffer = ArrayBuffer.Create 16
        let buffer= Buffer.from arrayBuffer
        arrayBuffer = buffer.buffer |> equal true

      testCase "buf.compare" <| fun _ -> 
        let buf1 = Buffer.from "ABC"
        let buf2 = Buffer.from "BCD"
        buf1.compare buf2 |> equal -1
      
      testCase "buf.compare" <| fun _ -> 
        let buf1 = Buffer.from [|1..9|]
        let buf2 = Buffer.from [|5;6;7;8;9;1;2;3;4|]
        buf1.compare(buf2,5,9,0,4) |> equal 0

      testCase "buf.copy" <| fun _ -> 
        let buf1 : byte []= !!Buffer.allocUnsafe 26
        let buf2 = Buffer.allocUnsafe(26).fill("!")
        for i in 0..25 do 
          buf1.[i] <- !!(i + 97)

        buf1?copy(buf2, 8,16,20)
        buf2.toString(Encoding.Ascii, 0, 25) |> equal "!!!!!!!!qrst!!!!!!!!!!!!!"

      testCase "buf.entries" <| fun _ -> 
        let buf = Buffer.from "buffer"
        buf.entries() 
          |> Seq.map( fun (idx,v) -> sprintf "%i,%i" idx v ) 
          |> String.concat "-"
          |> equal "0,98-1,117-2,102-3,102-4,101-5,114"
      
      testCase "buf.equals" <| fun _ -> 
        let buf1 = Buffer.from "ABC"
        let buf2 = Buffer.from("414243", Encoding.Hex)
        let buf3 = Buffer.from "ABCD"

        ( buf1.equals buf2 
          && buf1.equals buf3 = false) |> equal true

      testCase "buf.includes" <| fun _ -> 
        let buf = Buffer.from "this is a buffer"
        (
          buf.includes "this" 
          && buf.includes (Buffer.from "a buffer"))
          |> equal true

        
      testCase "buf.indexOf" <| fun _ -> 
        let buf = "this is a buffer"
        buf.IndexOf "is" |> equal 2

      testCase "buf.keys" <| fun _ -> 
        let buf = Buffer.from "buffer"
        buf.keys() 
          |> Seq.sum
          |> equal 15

      testCase "buf.lastIndexOf" <| fun _ -> 
        let buf = Buffer.from "this buffer is a buffer"
        buf.lastIndexOf (Buffer.from "buffer") |> equal 17
      
      testCase "buf.length" <| fun _ -> 
        let buf = Buffer.alloc 1234
        buf.length |> equal 1234

      testCase "buf.read" <| fun _ -> 
        let buf = Buffer.from [|1..8|]
        console.log (buf.readFloatBE 0)
        console.log (buf.readFloatLE 0)
        console.log (buf.readDoubleBE 0)
        console.log (buf.readDoubleLE 0)
        console.log (buf.readInt16BE 0)
        console.log (buf.readInt16LE 0)
        console.log (buf.readInt32BE 0)
        console.log (buf.readInt32LE 0)
        console.log (buf.readInt8 0)
        console.log (buf.readIntLE(0,1))
        console.log (buf.readIntBE(0,1))
        console.log (buf.readUInt8 0)
        console.log (buf.readUInt16BE 0)
        console.log (buf.readUInt16LE 0)
        console.log (buf.readUInt32BE 0)
        console.log (buf.readUInt32LE 0)
        console.log (buf.readUIntBE(0,1))
        console.log (buf.readUIntLE(0,1))
        testPassed()

      testCase "buf.slive" <| fun _ -> 
        let buf1 : byte []= !!Buffer.allocUnsafe 26
        for i in 0..25 do 
          buf1.[i] <- !!(i + 97)

        let buf1 : Node.Buffer.Buffer = !!buf1
        let buf2 = buf1.slice(0,3)
        buf2.toString(Encoding.Ascii, 0, buf2.length) |> equal "abc"

      testCase "buf.swap" <| fun _ -> 
        let buf1 = Buffer.from [|0x1; 0x2; 0x3; 0x4; 0x5; 0x6; 0x7; 0x8|]
        console.log(buf1.swap16())
        console.log(buf1.swap32())
        console.log(buf1.swap64())
        testPassed()

      testCase "buf.toJSON" <| fun _ -> 
        let buf = Buffer.from [|0x1;0x2;0x3;0x4;0x5|]
        let json = JS.JSON.stringify buf
        console.log json
        testPassed()

      testCase "buf.values" <| fun _ -> 
        let buf = Buffer.from "buffer"
        buf.values() 
          |> Seq.length
          |> equal 6

      testCase "buf.write" <| fun _ -> 
        let buf = Buffer.alloc 256
        let len = buf.write("\u00bd + \u00bc = \u00be",0)
        len |> equal 12

      testCase "buf.writeXX " <| fun _ -> 
        let buf = Buffer.allocUnsafe 8 
        buf.writeDoubleBE(123.456,0) |> ignore
        buf.writeDoubleLE(123.456,0) |> ignore
        let buf = Buffer.allocUnsafe 4 
        buf.writeFloatBE(float 0xcafebabe,0) |> ignore
        buf.writeFloatLE(float 0xcafebabe,0) |> ignore
        let buf = Buffer.allocUnsafe 2
        buf.writeInt8(2,0) |> ignore 
        let buf = Buffer.allocUnsafe 4
        buf.writeInt16BE(0x0102,0) |> ignore 
        buf.writeInt16LE(0x0102,0) |> ignore 
        buf.writeInt32BE(int32 0x01020304,0) |> ignore 
        buf.writeInt32LE(int32 0x01020304,0) |> ignore 
        let buf = Buffer.allocUnsafe 4
        buf.writeUInt8(0x3,0) |> ignore 
        let buf = Buffer.allocUnsafe 4
        buf.writeUInt16BE(0xdead,0) |> ignore 
        buf.writeUInt16LE(0xbeef,0) |> ignore 
        let buf = Buffer.allocUnsafe 4
        buf.writeUInt32BE(0xfeedface,0) |> ignore 
        buf.writeUInt32LE(0xfeedface,0) |> ignore 
        let buf = Buffer.allocUnsafe 6
        //buf.writeUIntBE(0x1234567890ab, 0, 6) |> ignore
        //buf.writeUIntLE(0x1234567890ab, 0, 6) |> ignore
        //let buf = Buffer.allocUnsafe 6
        //buf.writeIntBE((int64 0x1234567890ab,0,6) |> ignore 
        //buf.writeIntLE(int64 0x1234567890ab,0,6) |> ignore 
        testPassed()

      testCase "buffer.INSPECT_MAX_BYTES" <| fun _ -> 
          let buf = Buffer.from "ABC"
          console.log( buf.INSPECT_MAX_BYTES )
          testPassed()

      testCase "buffer.kMaxLength" <| fun _ -> 
          let buf = Buffer.from "ABC"
          console.log( buf.kMaxLength )
          testPassed()

      testCase "buffer.transcode" <| fun _ -> 
        let buf = Buffer.from("€")
        let tc = Node.Api.buffer.transcode(buf, Encoding.Utf8, Encoding.Ascii) 
        (string tc) |> equal "?"
    ]

  ]
