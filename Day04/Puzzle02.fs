module Puzzle02
open Shared

let ContainsAtLeastOneSetOfAnAdjacentDoubleNumber password =
    password
    |> Array.countBy id
    |> Array.filter (fun (_, count) -> count = 2)
    |> Array.length > 0

let IsPasswordValid password =
    let pwd = password.ToString().ToCharArray()
    ContainsOnlyIncreasingDigits pwd && ContainsAtLeastOneSetOfAnAdjacentDoubleNumber pwd