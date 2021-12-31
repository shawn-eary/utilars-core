namespace utilars_core.Controllers

open System.Collections.Generic
open System.Diagnostics

open Microsoft.AspNetCore.Mvc
// ©2013, 2021 - Shawn Eary
//
// Licensed via FCML:
// https://www.utilars.com/Home/FCML
open Microsoft.Extensions.Logging

open utilars_core.Models

module helper = 
   let toList (x : System.Collections.Generic.IEnumerable<efModels.sp_coloringPagesIndexResult>) = 
      let returnList = new System.Collections.Generic.List<efModels.sp_coloringPagesIndexResult>()
      for item in x do
         returnList.Add(item)
      returnList

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    member this.Index () =
        this.ViewData.["Message"] <- "Welcome to Utilars"
        this.View()
    member this.FCDL () =
        this.View()
    member this.UsedCarLot () =
        this.View()
    member this.FCML () =
        this.View()
    member this.LinuxBeef ()  =
        this.View()
    member this.Music ()  =
        this.View()
    member this.Pictures () =
        this.View()
    member this.Privacy () =
        this.View()
    member x.ColoringPages () =
        use someEFContext = new efModels.utilarsDBContext()
        // https://stackoverflow.com/questions/5095183/how-would-i-run-an-async-taskt-method-synchronously
        let coloringPagesModelIEnumerable :
            System.Collections.Generic.IEnumerable<efModels.sp_coloringPagesIndexResult> =
            someEFContext.Procedures.sp_coloringPagesIndexAsync().GetAwaiter().GetResult()
            :> System.Collections.Generic.IEnumerable<efModels.sp_coloringPagesIndexResult>
        let coloringPagesModelAsList = helper.toList coloringPagesModelIEnumerable
        x.View(model = coloringPagesModelAsList) :> ActionResult
    member x.Clipart() : ActionResult = 
       x.View() :> ActionResult
    member x.CheezeBlaster() : ActionResult = 
          x.View() :> ActionResult
    member x.Stories() : ActionResult = 
       x.View() :> ActionResult
    member x.InformationTechnology () : ActionResult = 
       x.View() :> ActionResult
    member x.StartURLTest () : ActionResult = 
       x.View() :> ActionResult
    member x.URLTest () : ActionResult = 
       x.View() :> ActionResult
    member x.MRoots () : ActionResult = 
       x.View() :> ActionResult

    [<ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)>]
    member this.Error () =
        let reqId = 
            if isNull Activity.Current then
                this.HttpContext.TraceIdentifier
            else
                Activity.Current.Id

        this.View({ RequestId = reqId })
    member x.About () =
        x.View() :> ActionResult
    member x.Values () = 
        x.View() :> ActionResult
    [<HttpGet>]
    member x.Contact () = 
        let theModel = new utilars_core.Models.MessageAttempt()
        x.View(theModel) :> ActionResult
    // References: 
    // http://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient.credentials.aspx
    [<HttpPost>]
    member x.Contact (someMessageAttempt : utilars_core.Models.MessageAttempt) : ActionResult =        
        if x.ModelState.IsValid then
           let response = someMessageAttempt.ChallengeResponse
           let expected = x.Request.Form.Item("Expected")
           let result = 
               logicLibrary.imgGenerator.responseIsValid response expected
           if result then
               x.ViewData.["Message"] <- 
                   "[Sanitized]"
               x.View("MessageToUser") :> ActionResult
            else
                x.ViewData.["MessageToUser"] <- 
                    "It appears that you didn't enter the correct " +
                    "response for the number challenge."
                let expectedAuth = logicLibrary.imgGenerator.getImages(0)
                someMessageAttempt.Base64Digits <- fst(expectedAuth)
                someMessageAttempt.Expected <- snd(expectedAuth)
                x.View("Contact", someMessageAttempt) :> ActionResult
         else
             (* User entered bogus data.  Let him/her try again *)
             let expectedAuth = logicLibrary.imgGenerator.getImages(0)                   
             someMessageAttempt.Base64Digits <- fst(expectedAuth)
             someMessageAttempt.Expected <- snd(expectedAuth)
             x.View("Contact", someMessageAttempt) :> ActionResult
