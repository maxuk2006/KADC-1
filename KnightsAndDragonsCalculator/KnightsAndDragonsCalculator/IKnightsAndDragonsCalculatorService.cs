using KnightsAndDragonsCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace KnightsAndDragonsCalculator
{
    [ServiceContract]
    public interface IKnightsAndDragonsCalculatorService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "calc/{armorName}/{targetLevel}")]
        CalculatorResults CalculateStats(string armorName, string targetLevel);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "calc/{armorName}/{targetLevel}/{feedArmorName}")]
        CalculatorResults CalculateStatsAndCost(string armorName, string targetLevel, string feedArmorName);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "names")]
        List<string> GetArmorNames();
    }
}
