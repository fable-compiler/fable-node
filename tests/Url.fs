module Tests.Url

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.DynamicExtensions
open Node.OS
open Util

let URL = Node.Api.URL

let makeUrl input (b:string) :Node.Url.Url<string> = URL.Create(input, !^b)

let tests : Test = 
  testList "Url" [
  
    testList "new" [
      testCase "from string" <| fun _ ->
          let url = makeUrl "/foo" "https://example.org/"
          url.href = "https://example.org/foo" |> equal true

      testCase "unicode" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://測試")
          (url.href = "https://xn--g6w251d/" // with node compiled with ICU enabled
            || url.href = "https://測試") |> equal true
    ]

    testList "hash" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/foo#bar")
          url.hash = "#bar" |> equal true
      
      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/foo#bar")
          url.hash <- "baz"
          url.href = "https://example.org/foo#baz" |> equal true
    ]

    testList "host" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:81/foo")
          url.host = "example.org:81" |> equal true
      
      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:81/foo")
          url.host <- "example.com:82"
          url.href = "https://example.com:82/foo" |> equal true
    ]

    testList "hostname" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:81/foo")
          url.hostname = "example.org" |> equal true
      
      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:81/foo")
          url.hostname <- "example.com:82"
          url.href = "https://example.com:81/foo" |> equal true
    ]

    testList "href" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/foo")
          url.href = "https://example.org/foo" |> equal true
      
      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/bar")
          url.href <- "https://example.org/foo"
          url.href = "https://example.org/foo" |> equal true
    ]

    testList "origin" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/foo/bar?baz")
          url.origin = "https://example.org" |> equal true
      
      testCase "unicode" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://測試")
          url.origin = "https://xn--g6w251d" |> equal true
    ]

    testList "password" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://abc:xyz@example.com")
          url.password = "xyz" |> equal true

      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://abc:xyz@example.com")
          url.password <- "123" 
          url.href = "https://abc:123@example.com/" |> equal true  
    ]

    testList "pathname" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/abc/xyz?123")
          url.pathname = "/abc/xyz" |> equal true

      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/abc/xyz?123")
          url.pathname <- "/abcdef" 
          url.href = "https://example.org/abcdef?123" |> equal true  
    ]

    testList "port" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:8888")
          url.port = "8888" |> equal true

      testCase "443" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:8888")
          url.port <- "443"
          (url.port = "" && url.href = "https://example.org/") |> equal true  

      testCase "1234" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:8888")
          url.port <- "1234"
          (url.port = "1234" && url.href = "https://example.org:1234/") |> equal true  

      testCase "abcd" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:8888")
          url.port <- "1234"
          url.port <- "abcd"
          (url.port = "1234" && url.href = "https://example.org:1234/") |> equal true  

      testCase "abcd" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org:8888")
          url.port <- "1234.5678"
          (url.port = "1234" && url.href = "https://example.org:1234/") |> equal true  
    ]

    testList "protocol" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org")
          url.protocol = "https:" |> equal true

      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org")
          url.protocol <- "ftp"
          url.href = "ftp://example.org/" |> equal true  
    ]

    testList "search" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/abc?123")
          url.search = "?123" |> equal true

      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://example.org/abc?123")
          url.search = "?123" |> equal true
          url.search <- "abc=xyz"
          url.href = "https://example.org/abc?abc=xyz" |> equal true  
    ]

    testList "username" [
      testCase "get" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://abc:xyz@example.com")
          url.username = "abc" |> equal true

      testCase "set" <| fun _ ->
          let url : Node.Url.Url<string> = URL.Create("https://abc:xyz@example.com")
          url.username <- "123" 
          url.href = "https://123:xyz@example.com/" |> equal true  
    ]

  ]