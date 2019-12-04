module Puzzle02
    
let ContainsOnlyIncreasingDigits password =
    let orderedPwd = password |> Array.sort
    password = orderedPwd

let ContainsAtLeastOneSetOfAnAdjacentDoubleNumber password =
    password
    |> Array.countBy id
    |> Array.filter (fun (_, count) -> count = 2)
    |> Array.length > 0

let IsPasswordValid password =
    let pwd = password.ToString().ToCharArray()
    ContainsOnlyIncreasingDigits pwd && ContainsAtLeastOneSetOfAnAdjacentDoubleNumber pwd

let GeneratePossiblePasswords min max =
    [|min..max|]

let GeneratePossibleValidPasswords min max =
    GeneratePossiblePasswords min max
    |> Array.filter IsPasswordValid