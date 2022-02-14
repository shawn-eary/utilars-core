namespace utilars_core.Controllers

open System.Diagnostics

open Microsoft.AspNetCore.Mvc
// ©2022 - Shawn Eary
//
// Licensed via FCML:
// https://www.utilars.com/Home/FCML
open Microsoft.Extensions.Logging

open utilars_core.Models

type StoryTimeController (logger : ILogger<StoryTimeController>) =
    inherit Controller()

    member this.Index () =
        this.View()
    member this.Credits () =
        this.View()
