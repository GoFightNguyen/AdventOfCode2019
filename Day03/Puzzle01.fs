module Puzzle01

type DirectionCommand = {
        Direction : char
        Distance : int
    }

type IntersectingPoint = {
    X : int
    Y : int
    ManhattanDistanceFromCentralPoint : int
}

let ParsePath (direction:string) =
    let dir = direction.[0]
    let distance = direction.[1..]
    { Direction = dir; Distance = int distance}

let Flatten directionCommand =
    [|1..directionCommand.Distance|]
    |> Array.map (fun d -> directionCommand.Direction)

let NextPoint direction x y =
    if direction = 'U' then
        x + 1,y
    else if direction = 'D' then
        x - 1,y
    else if direction = 'R' then
        x,y + 1
    else
        x,y - 1

let MapWire (path:string[]) =
    let mutable x,y = 0,0

    path 
    |> Array.map ParsePath
    |> Array.collect Flatten
    |> Array.map (fun direction ->
            let newX, newY = NextPoint direction x y
            x <- newX
            y <- newY

            x,y
        )

let FindIntersectingPoints pathPoints1 pathPoints2 =
    Set.ofArray pathPoints1
    |> Set.intersect (Set.ofArray pathPoints2)

let CalculateManhattanDistance (x,y) = abs x + abs y

let FindClosest points =
    points
    |> Set.map (fun p -> {X = fst p; Y = snd p; ManhattanDistanceFromCentralPoint = CalculateManhattanDistance p })
    |> Set.toArray
    |> Array.sortBy (fun ip -> ip.ManhattanDistanceFromCentralPoint)
    |> Array.head