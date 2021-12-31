// ©2013 - Shawn Eary
namespace FsWeb.Controllers

open System.IO
open System.Linq
open System.Collections.Generic
open logicLibrary
open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Mvc.Routing
open utilars_core.Models

module blogHelper = 
   let toItem (x : System.Collections.Generic.IEnumerable<efModels.sp_getBlogEntryResult>) =
      x.First()
   let toList (x : System.Collections.Generic.IEnumerable<efModels.sp_getBlogListResult>) = 
      let returnList = new System.Collections.Generic.List<efModels.sp_getBlogListResult>()
      for item in x do
         returnList.Add(item)
      returnList


// [<HandleError>]
type BlogController() =
   inherit Controller()

   // https://www.mikesdotnetting.com/Article/302/server-mappath-equivalent-in-asp-net-core
   [<HttpGet>]
   member x.Item userName year month day id : ActionResult =
      use someEFContext = new efModels.utilarsDBContext()
      //  https://stackoverflow.com/questions/5095183/how-would-i-run-an-async-taskt-method-synchronously
      let blogModelIEnumerable :
         System.Collections.Generic.IEnumerable<efModels.sp_getBlogEntryResult> = 
         someEFContext.Procedures.sp_getBlogEntryAsync(userName, year, month, day, id).GetAwaiter().GetResult()
         :> System.Collections.Generic.IEnumerable<efModels.sp_getBlogEntryResult>
      let efBlogEntry : efModels.sp_getBlogEntryResult = blogHelper.toItem blogModelIEnumerable      
      // let someHelper = new System.Web.Mvc.UrlHelper(x.ControllerContext.RequestContext)
      
      (* http://forums.asp.net/t/1164934.aspx *)
      let theURI = efBlogEntry.URI
      // BEGIN Hack
      let absoluteFilePath =
          "wwwroot/" +
          System.Text.RegularExpressions.Regex.Replace(theURI, "^~\/", "")
      // This stopped working when I migrated from .NET 4.5 (Old) to .NET 6 (Core)
      // let absoluteFilePath = x.Server.MapPath(theURI)
      // END   Hack
      let theBlogEntry : BlogEntry = 
         new BlogEntry(
            efBlogEntry.title, File.ReadAllText(absoluteFilePath)
      )
      x.View(model = theBlogEntry) :> ActionResult
   [<HttpGet>]
   member x.Index (userName : string) : ActionResult =
      use someEFContext = new efModels.utilarsDBContext()
      let blogListIEnumerable :
         System.Collections.Generic.IEnumerable<efModels.sp_getBlogListResult> =
         someEFContext.Procedures.sp_getBlogListAsync().GetAwaiter().GetResult()
         :> System.Collections.Generic.IEnumerable<efModels.sp_getBlogListResult>
      let efBlogList = blogHelper.toList blogListIEnumerable
      x.View(model = efBlogList) :> ActionResult




        