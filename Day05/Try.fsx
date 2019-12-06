open System
#load "Puzzle01.fs"

open Puzzle01

let program = [|1002;4;3;4;33|]
let allParts = program.[0].ToString()
let startingIndexOfOpcode = allParts.Length - 2
let opCode = int allParts.[startingIndexOfOpcode..]
// allParts.[0..startingIndexOfOpcode-1].Split(string.Empty) |> Array.map int |> Array.rev

// allParts.[0..startingIndexOfOpcode-1].ToCharArray() |> Array.map string |> Array.map int |> Array.rev
allParts.[0..startingIndexOfOpcode-1].ToCharArray() |> Array.map (string >> int) |> Array.rev
// System.String.Join("", allParts)
// Array.