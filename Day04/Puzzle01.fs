module Puzzle01

let ContainsConsecutiveSameDigits password =
    password
    |> Array.countBy id
    |> Array.filter (fun (_, count) -> count > 1)
    |> Array.length > 0
    
let ContainsOnlyIncreasingDigits password =
    let orderedPwd = password |> Array.sort
    password = orderedPwd

let IsPasswordValid password =
    let pwd = password.ToString().ToCharArray()
    ContainsOnlyIncreasingDigits pwd && ContainsConsecutiveSameDigits pwd

let GeneratePossiblePasswords min max =
    [|min..max|]

let GeneratePossibleValidPasswords min max =
    GeneratePossiblePasswords min max
    |> Array.filter IsPasswordValid