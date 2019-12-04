module Shared

let ContainsOnlyIncreasingDigits password =
    let orderedPwd = password |> Array.sort
    password = orderedPwd

let GeneratePossiblePasswords min max =
    [|min..max|]

let GeneratePossibleValidPasswords isPasswordValid min max =
    GeneratePossiblePasswords min max
    |> Array.filter isPasswordValid