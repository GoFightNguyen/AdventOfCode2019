module Puzzle01
open Shared

let ContainsConsecutiveSameDigits password =
    password
    |> Array.countBy id
    |> Array.filter (fun (_, count) -> count > 1)
    |> Array.length > 0

let IsPasswordValid password =
    let pwd = password.ToString().ToCharArray()
    ContainsOnlyIncreasingDigits pwd && ContainsConsecutiveSameDigits pwd