module Puzzle01
open System.Collections.Generic
open System.Linq

type OrbitCountChecksum = {
    Direct : int
    Indirect : int
}

type CenterOfMass = { Name:string }
type Orbiting = {Name:string}
type UniverseObject =
    | CenterOfMass of CenterOfMass
    | Orbiting of UniverseObject
    | OrbitingName of string * UniverseObject

let Graph (orbitMap:string list) = 
    let graph = new Dictionary<string, UniverseObject>()

    let orbitingCOM, indirectlyOrbitingCOM = 
        orbitMap
        |> List.map (fun o -> 
                let parts = o.Split(")")
                parts.[0], parts.[1]
            )
        |> List.groupBy fst
        |> List.map (fun d -> 
                let dependencies =
                    snd d
                    |> List.map snd
                fst d, dependencies            
            )
        |> List.partition (fun d -> (fst d) = "COM")

    let orbitsWithComFirst = orbitingCOM @ indirectlyOrbitingCOM                

    let rec buildDependencies key =
        let univObject = graph.[key]
        let dependencies = orbitsWithComFirst |> List.filter (fun d -> (fst d) = key) |> List.collect snd
        for d in dependencies do
            let uo = Orbiting univObject
            graph.Add(d, uo)
            buildDependencies d

    let com = CenterOfMass { Name = "COM" }
    graph.Add("COM", com)
    buildDependencies "COM"    

    graph

let rec CalculateDistanceToCenterOfMass (universeObject:UniverseObject) =
    match universeObject with
    | CenterOfMass _ -> 0
    | Orbiting orbiting -> 1 + CalculateDistanceToCenterOfMass orbiting

let CalculateOrbitCountChecksum (orbitGraph:Dictionary<string,UniverseObject>) =
    let all = 
        orbitGraph.ToArray()
        |> Array.sumBy (fun d -> d.Value |> CalculateDistanceToCenterOfMass)
    let numberOfOrbits = orbitGraph.Count - 1   //  have to exclude the CenterOfMass
    { Direct = numberOfOrbits; Indirect = all - numberOfOrbits }







let Graph2 centerOfMassName (orbitMap:string list) =
    let graph = new Dictionary<string, UniverseObject>()

    let orbitingCOM, indirectlyOrbitingCOM = 
        orbitMap
        |> List.map (fun o -> 
                let parts = o.Split(")")
                parts.[0], parts.[1]
            )
        |> List.groupBy fst
        |> List.map (fun d -> 
                let dependencies =
                    snd d
                    |> List.map snd
                fst d, dependencies            
            )
        |> List.partition (fun d -> (fst d) = centerOfMassName)

    let orbitsWithComFirst = orbitingCOM @ indirectlyOrbitingCOM                

    let rec buildDependencies key =
        let univObject = graph.[key]
        let dependencies = orbitsWithComFirst |> List.filter (fun d -> (fst d) = key) |> List.collect snd
        for d in dependencies do
            let uo = OrbitingName (d, univObject)
            graph.Add(d, uo)
            buildDependencies d

    let com = CenterOfMass { Name = centerOfMassName }
    graph.Add(centerOfMassName, com)
    buildDependencies centerOfMassName    

    graph


let rec CalculateDistanceToPlace placeName (universeObject:UniverseObject) =
    match universeObject with
    | CenterOfMass _ -> 0
    | OrbitingName (name, orbiting) -> 
        if name = placeName then 0
        else 1 + CalculateDistanceToPlace placeName orbiting

let CalculateDistance (orbitGraph:Dictionary<string,UniverseObject>) =
    let rec allUniverseObjects (collection:List<string>) universeObject = 
        match universeObject with
        | CenterOfMass com -> collection.Add com.Name
        | OrbitingName (name,uo) -> 
            collection.Add name
            allUniverseObjects collection uo 

    let from = orbitGraph.["YOU"]
    let fromUniverseObjects = new List<string>();
    allUniverseObjects fromUniverseObjects from

    let destination = orbitGraph.["SAN"]
    let destinationUniverseObjects = new List<string>()
    allUniverseObjects destinationUniverseObjects destination

    let shared = fromUniverseObjects.Intersect(destinationUniverseObjects)
    let sharedWithDistances = new Dictionary<string,int>()
    for s in shared do
        let fromDistance = CalculateDistanceToPlace s from
        let destinationDistance = CalculateDistanceToPlace s destination
        sharedWithDistances.Add(s, fromDistance + destinationDistance)

    // subtract two because we are calculating the distance between the objects orbited,
    // not YOU and SAN
    sharedWithDistances.OrderBy(fun s -> s.Value).First().Value - 2  
