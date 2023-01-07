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

type SupportController (logger : ILogger<SupportController>) =
    inherit Controller()

    // member this.Index () =
    //     x.
    //     this.ViewData.["Message"] <- "Welcome to Utilars™"
    //     this.View()
    [<HttpGet>]
    member this.Index () =
        this.ViewData.["Message"] <- "Utilars™ Support Request"

        // https://www.c-sharpcorner.com/article/redirect-action-result-in-asp-net-core-mvc/
        this.RedirectToAction("Ticket","Support")
    member x.Ticket (product : string) = 
        let theModel = new utilars_core.Models.TicketRequest()
        x.View(theModel) :> ActionResult
    // References: 
    // http://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient.credentials.aspx
    [<HttpPost>]
    member x.Ticket (someTicketRequest : utilars_core.Models.TicketRequest) : ActionResult =        
        if x.ModelState.IsValid then
           let response = someTicketRequest.ChallengeResponse
           let expected = x.Request.Form.Item("Expected")
           let result = 
               logicLibrary.imgGenerator.responseIsValid response expected
           if result then
               let thePassword = "[Sanitized]"
               let forwardingAccount:string = "[Sanitized]"
               let theServer:string = "[Sanitized]"
               let thePort:int = -1


               let userEmail = someTicketRequest.Email
               let userMsg = someTicketRequest.Message
               let theProduct: string = "OATS 1.0"

               let combinedMsg = 
                  "Utilars™ Support Request\n\r" +
                  "\n\r" + 
                  "Customer Email   : " + userEmail + "\n\r" + 
                  "Customer Name    : " + someTicketRequest.Name + "\n\r" + 
                  "Customer Product : " + theProduct + "\n\r" + 
                  "Message : \n\r" + userMsg
               let myEmailClient = new SmtpClient(theServer, thePort)
               myEmailClient.EnableSsl <- true            
               myEmailClient.UseDefaultCredentials <- false

               let myCredentials = 
                   new NetworkCredential(
                      forwardingAccount, 
                      thePassword, 
                      ""
                    )
               myEmailClient.Credentials <- myCredentials
               myEmailClient.Send(
                   forwardingAccount,
                   "[Sanitized]," + userEmail,
                   "msg From:" + userEmail, 
                   combinedMsg
                )

               x.ViewData.["Message"] <- 
                   "Your request has been forwarded to support"
               x.View("MessageToUser") :> ActionResult
            else
                x.ViewData.["MessageToUser"] <- 
                    "It appears that you didn't enter the correct " +
                    "response for the number challenge."
                let expectedAuth = logicLibrary.imgGenerator.getImages(0)
                someTicketRequest.Base64Digits <- fst(expectedAuth)
                someTicketRequest.Expected <- snd(expectedAuth)
                x.View("Ticket", someTicketRequest) :> ActionResult
         else
             (* User entered bogus data.  Let him/her try again *)
             let expectedAuth = logicLibrary.imgGenerator.getImages(0)                   
             someTicketRequest.Base64Digits <- fst(expectedAuth)
             someTicketRequest.Expected <- snd(expectedAuth)
             x.View("Ticket", someTicketRequest) :> ActionResult
