module Puzzle02

let FindStepsTo point pathPoints =
    let index = 
        pathPoints
        |> Array.findIndex (fun p -> p = point)

    index + 1

let FindClosestWithMinimalSignalDelay pathPoints1 pathPoints2 points =
    points
    |> Set.toArray
    |> Array.map (fun p ->
            let stepsTo1 = FindStepsTo p pathPoints1
            let stepsTo2 = FindStepsTo p pathPoints2
            stepsTo1 + stepsTo2
        )
    |> Array.min    