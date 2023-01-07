namespace utilars_core.Controllers

open System.Collections.Generic
open System.Diagnostics

open Microsoft.AspNetCore.Mvc
// ©2013, 2021-2023 - Shawn Eary
//
// Licensed via FCML:
// https://www.utilars.com/Home/FCML
open Microsoft.Extensions.Logging

open utilars_core.Models

open System.Net.Mail
open System.Net

type DonateController (logger : ILogger<SupportController>) =
    inherit Controller()

    [<HttpGet>]
    member this.Index () =
        this.ViewData.["Message"] <- "Thank you"
        this.View() :> ActionResult
