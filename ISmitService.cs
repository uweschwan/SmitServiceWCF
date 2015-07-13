using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SmitService.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmitService" in both code and config file together.
    
    [ServiceContract]
    public interface ISmitService
    {
        //[OperationContract]
        //[OperationContractAttribute(Action = "urn:getCustomers", ReplyAction = "*")]
        //getCustomersResponse getCustomers(getCustomersRequest request);

        [OperationContractAttribute(Action = "urn:#getCustomers", ReplyAction = "*")]
        getCustomersResponse getCustomers(getCustomersRequest request);

        //[OperationContract]
        //[OperationContractAttribute(Action = "urn:getCustomerCount", ReplyAction = "*")]
        //getCustomerCountResponse getCustomerCount(getCustomerCountRequest request);

        [OperationContractAttribute(Action = "urn:#getCustomerCount", ReplyAction = "*")]
        getCustomerCountResponse getCustomerCount(getCustomerCountRequest request);

        //[OperationContract]
        //[OperationContractAttribute(Action = "urn:getCustomerVehicles", ReplyAction = "*")]
        //getCustomerVehiclesResponse getCustomerVehicles(getCustomerVehiclesRequest request);

        [OperationContractAttribute(Action = "urn:#getCustomerVehicles", ReplyAction = "*")]
        getCustomerVehiclesResponse getCustomerVehicles(getCustomerVehiclesRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getCustomerVehicleCount", ReplyAction = "*")]
        getCustomerVehicleCountResponse getCustomerVehicleCount(getCustomerVehicleCountRequest request);

        //[OperationContractAttribute(Action = "urn:getCustomerVehicleCount", ReplyAction = "*")]
        //getCustomerVehicleCountResponse getCustomerVehicleCount(getCustomerVehicleCountRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#searchSalesVehicles", ReplyAction = "*")]
        searchSalesVehiclesResponse searchSalesVehicles(searchSalesVehiclesRequest request);

        //[OperationContractAttribute(Action = "urn:searchSalesVehicles", ReplyAction = "*")]
        //searchSalesVehiclesResponse searchSalesVehicles(searchSalesVehiclesRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getSalesVehicleDetail", ReplyAction = "*")]
        getSalesVehicleDetailResponse getSalesVehicleDetail(getSalesVehicleDetailRequest request);

        //[OperationContractAttribute(Action = "urn:getSalesVehicleDetail", ReplyAction = "*")]
        //getSalesVehicleDetailResponse getSalesVehicleDetail(getSalesVehicleDetailRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getAllInvoiceCount", ReplyAction = "*")]
        getAllInvoiceCountResponse getAllInvoiceCount(getAllInvoiceCountRequest request);

        //[OperationContractAttribute(Action = "urn:getAllInvoiceCount", ReplyAction = "*")]
        //getAllInvoiceCountResponse getAllInvoiceCount(getAllInvoiceCountRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getAllInvoices", ReplyAction = "*")]
        getAllInvoicesResponse getAllInvoices(getAllInvoicesRequest request);

        //[OperationContractAttribute(Action = "urn:getAllInvoices", ReplyAction = "*")]
        //getAllInvoicesResponse getAllInvoices(getAllInvoicesRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:getChangedCustomerCount", ReplyAction = "*")]
        getCustomerCountResponse getChangedCustomerCount(getCustomerCountRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getChangedCustomers", ReplyAction = "*")]
        getCustomersResponse getChangedCustomers(getCustomersRequest request);

        //[OperationContractAttribute(Action = "urn:getChangedCustomers", ReplyAction = "*")]
        //getCustomersResponse getChangedCustomers(getCustomersRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:getChangedCustomerVehicleCount", ReplyAction = "*")]
        getCustomerVehicleCountResponse getChangedCustomerVehicleCount(getCustomerVehicleCountRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:getChangedCustomerVehicles", ReplyAction = "*")]
        getCustomerVehiclesResponse getChangedCustomerVehicles(getCustomerVehiclesRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#createNewCustomer", ReplyAction = "*")]
        createNewCustomerResponse createNewCustomer(createNewCustomerRequest request);

        //[OperationContractAttribute(Action = "urn:createNewCustomer", ReplyAction = "*")]
        //createNewCustomerResponse createNewCustomer(createNewCustomerRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#changeCustomer", ReplyAction = "*")]
        changeCustomerResponse changeCustomer(changeCustomerRequest request);

        //[OperationContractAttribute(Action = "urn:changeCustomer", ReplyAction = "*")]
        //changeCustomerResponse changeCustomer(changeCustomerRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#deleteCustomer", ReplyAction = "*")]
        deleteCustomerResponse deleteCustomer(deleteCustomerRequest request);

        //[OperationContractAttribute(Action = "urn:deleteCustomer", ReplyAction = "*")]
        //deleteCustomerResponse deleteCustomer(deleteCustomerRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getNewInvoice", ReplyAction = "*")]
        getNewInvoiceResponse getNewInvoices(getNewInvoiceRequest request);

        //[OperationContractAttribute(Action = "urn:getNewInvoice", ReplyAction = "*")]
        //getNewInvoiceResponse getNewInvoices(getNewInvoiceRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#getNewInvoicesCount", ReplyAction = "*")]
        getNewInvoicesCountResponse getNewInvoicesCount(getNewInvoicesCountRequest request);

        //[OperationContractAttribute(Action = "urn:getNewInvoicesCount", ReplyAction = "*")]
        //getNewInvoicesCountResponse getNewInvoicesCount(getNewInvoicesCountRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#changeCustomerVehicle", ReplyAction = "*")]
        changeCustomerVehicleResponse changeCustomerVehicle(changeCustomerVehicleRequest request);

        //[OperationContractAttribute(Action = "urn:changeCustomerVehicle", ReplyAction = "*")]
        //changeCustomerVehicleResponse changeCustomerVehicle(changeCustomerVehicleRequest request);

        //[OperationContract]
        [OperationContractAttribute(Action = "urn:#deleteCustomerVehicle", ReplyAction = "*")]
        deleteCustomerVehicleResponse deleteCustomerVehicle(deleteCustomerVehicleRequest request);

        //[OperationContractAttribute(Action = "urn:deleteCustomerVehicle", ReplyAction = "*")]
        //deleteCustomerVehicleResponse deleteCustomerVehicle(deleteCustomerVehicleRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("ExportSchema")]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class Exception : object, System.Xml.Serialization.IXmlSerializable
    {

        private System.Xml.XmlNode[] nodesField;

        private static System.Xml.XmlQualifiedName typeName = new System.Xml.XmlQualifiedName("Exception", "http://www.fmade.at");

        public System.Xml.XmlNode[] Nodes
        {
            get
            {
                return this.nodesField;
            }
            set
            {
                this.nodesField = value;
            }
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.nodesField = System.Runtime.Serialization.XmlSerializableServices.ReadNodes(reader);
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            System.Runtime.Serialization.XmlSerializableServices.WriteNodes(writer, this.Nodes);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public static System.Xml.XmlQualifiedName ExportSchema(System.Xml.Schema.XmlSchemaSet schemas)
        {
            System.Runtime.Serialization.XmlSerializableServices.AddDefaultSchema(schemas, typeName);
            return typeName;
        }
    }
}


[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class changeCustomerRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "changeCustomer", Namespace = "http://www.fmade.at")]
    public changeCustomerRequestBody Body;

    public changeCustomerRequest()
    {
    }

    public changeCustomerRequest(changeCustomerRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class changeCustomerRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public changeCustomerRequestBody()
    {
    }

    public changeCustomerRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class changeCustomerResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "changeCustomerResponse", Namespace = "http://www.fmade.at")]
    public changeCustomerResponseBody Body;

    public changeCustomerResponse()
    {
    }

    public changeCustomerResponse(changeCustomerResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class changeCustomerResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public changeCustomerResponseBody()
    {
    }

    public changeCustomerResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class deleteCustomerRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "deleteCustomer", Namespace = "http://www.fmade.at")]
    public deleteCustomerRequestBody Body;

    public deleteCustomerRequest()
    {
    }

    public deleteCustomerRequest(deleteCustomerRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class deleteCustomerRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public deleteCustomerRequestBody()
    {
    }

    public deleteCustomerRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class deleteCustomerResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "deleteCustomerResponse", Namespace = "http://www.fmade.at")]
    public deleteCustomerResponseBody Body;

    public deleteCustomerResponse()
    {
    }

    public deleteCustomerResponse(deleteCustomerResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class deleteCustomerResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public deleteCustomerResponseBody()
    {
    }

    public deleteCustomerResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getCustomerCount", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getCustomerCountRequest
{

    public getCustomerCountRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getCustomerCountResponse", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getCustomerCountResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "")]
    public int @return;

    public getCustomerCountResponse()
    {
    }

    public getCustomerCountResponse(int @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getCustomersRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getCustomers", Namespace = "http://www.fmade.at")]
    public getCustomersRequestBody Body;

    public getCustomersRequest()
    {
    }

    public getCustomersRequest(getCustomersRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getCustomersRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
    public int arg0;

    [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
    public int arg1;

    public getCustomersRequestBody()
    {
    }

    public getCustomersRequestBody(int arg0, int arg1)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getCustomersResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getCustomersResponse", Namespace = "http://www.fmade.at")]
    public getCustomersResponseBody Body;

    public getCustomersResponse()
    {
    }

    public getCustomersResponse(getCustomersResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getCustomersResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public getCustomersResponseBody()
    {
    }

    public getCustomersResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class changeCustomerVehicleRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "changeCustomerVehicle", Namespace = "http://www.fmade.at")]
    public changeCustomerVehicleRequestBody Body;

    public changeCustomerVehicleRequest()
    {
    }

    public changeCustomerVehicleRequest(changeCustomerVehicleRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class changeCustomerVehicleRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public changeCustomerVehicleRequestBody()
    {
    }

    public changeCustomerVehicleRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class changeCustomerVehicleResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "changeCustomerVehicleResponse", Namespace = "http://www.fmade.at")]
    public changeCustomerVehicleResponseBody Body;

    public changeCustomerVehicleResponse()
    {
    }

    public changeCustomerVehicleResponse(changeCustomerVehicleResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class changeCustomerVehicleResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public changeCustomerVehicleResponseBody()
    {
    }

    public changeCustomerVehicleResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class deleteCustomerVehicleRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "deleteCustomerVehicle", Namespace = "http://www.fmade.at")]
    public deleteCustomerVehicleRequestBody Body;

    public deleteCustomerVehicleRequest()
    {
    }

    public deleteCustomerVehicleRequest(deleteCustomerVehicleRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class deleteCustomerVehicleRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public deleteCustomerVehicleRequestBody()
    {
    }

    public deleteCustomerVehicleRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class deleteCustomerVehicleResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "deleteCustomerVehicleResponse", Namespace = "http://www.fmade.at")]
    public deleteCustomerVehicleResponseBody Body;

    public deleteCustomerVehicleResponse()
    {
    }

    public deleteCustomerVehicleResponse(deleteCustomerVehicleResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class deleteCustomerVehicleResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public deleteCustomerVehicleResponseBody()
    {
    }

    public deleteCustomerVehicleResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getCustomerVehicleCount", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getCustomerVehicleCountRequest
{

    public getCustomerVehicleCountRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getCustomerVehicleCountResponse", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getCustomerVehicleCountResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "")]
    public int @return;

    public getCustomerVehicleCountResponse()
    {
    }

    public getCustomerVehicleCountResponse(int @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getCustomerVehiclesRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getCustomerVehicles", Namespace = "http://www.fmade.at")]
    public getCustomerVehiclesRequestBody Body;

    public getCustomerVehiclesRequest()
    {
    }

    public getCustomerVehiclesRequest(getCustomerVehiclesRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getCustomerVehiclesRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
    public int arg0;

    [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
    public int arg1;

    public getCustomerVehiclesRequestBody()
    {
    }

    public getCustomerVehiclesRequestBody(int arg0, int arg1)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getCustomerVehiclesResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getCustomerVehiclesResponse", Namespace = "http://www.fmade.at")]
    public getCustomerVehiclesResponseBody Body;

    public getCustomerVehiclesResponse()
    {
    }

    public getCustomerVehiclesResponse(getCustomerVehiclesResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getCustomerVehiclesResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public getCustomerVehiclesResponseBody()
    {
    }

    public getCustomerVehiclesResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class changeContactPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "changeContactPerson", Namespace = "http://www.fmade.at")]
    public changeContactPersonRequestBody Body;

    public changeContactPersonRequest()
    {
    }

    public changeContactPersonRequest(changeContactPersonRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class changeContactPersonRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public changeContactPersonRequestBody()
    {
    }

    public changeContactPersonRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class changeContactPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "changeContactPersonResponse", Namespace = "http://www.fmade.at")]
    public changeContactPersonResponseBody Body;

    public changeContactPersonResponse()
    {
    }

    public changeContactPersonResponse(changeContactPersonResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class changeContactPersonResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public changeContactPersonResponseBody()
    {
    }

    public changeContactPersonResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class deleteContactPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "deleteContactPerson", Namespace = "http://www.fmade.at")]
    public deleteContactPersonRequestBody Body;

    public deleteContactPersonRequest()
    {
    }

    public deleteContactPersonRequest(deleteContactPersonRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class deleteContactPersonRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public deleteContactPersonRequestBody()
    {
    }

    public deleteContactPersonRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class deleteContactPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "deleteContactPersonResponse", Namespace = "http://www.fmade.at")]
    public deleteContactPersonResponseBody Body;

    public deleteContactPersonResponse()
    {
    }

    public deleteContactPersonResponse(deleteContactPersonResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class deleteContactPersonResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public deleteContactPersonResponseBody()
    {
    }

    public deleteContactPersonResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getContactPersonCount", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getContactPersonCountRequest
{

    public getContactPersonCountRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getContactPersonCountResponse", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getContactPersonCountResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "")]
    public int @return;

    public getContactPersonCountResponse()
    {
    }

    public getContactPersonCountResponse(int @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getContactPersonsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getContactPersons", Namespace = "http://www.fmade.at")]
    public getContactPersonsRequestBody Body;

    public getContactPersonsRequest()
    {
    }

    public getContactPersonsRequest(getContactPersonsRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getContactPersonsRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
    public int arg0;

    [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
    public int arg1;

    public getContactPersonsRequestBody()
    {
    }

    public getContactPersonsRequestBody(int arg0, int arg1)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getContactPersonsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getContactPersonsResponse", Namespace = "http://www.fmade.at")]
    public getContactPersonsResponseBody Body;

    public getContactPersonsResponse()
    {
    }

    public getContactPersonsResponse(getContactPersonsResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getContactPersonsResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public getContactPersonsResponseBody()
    {
    }

    public getContactPersonsResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class searchSalesVehiclesRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "searchSalesVehicles", Namespace = "http://www.fmade.at")]
    public searchSalesVehiclesRequestBody Body;

    public searchSalesVehiclesRequest()
    {
    }

    public searchSalesVehiclesRequest(searchSalesVehiclesRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class searchSalesVehiclesRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public searchSalesVehiclesRequestBody()
    {
    }

    public searchSalesVehiclesRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class searchSalesVehiclesResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "searchSalesVehiclesResponse", Namespace = "http://www.fmade.at")]
    public searchSalesVehiclesResponseBody Body;

    public searchSalesVehiclesResponse()
    {
    }

    public searchSalesVehiclesResponse(searchSalesVehiclesResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class searchSalesVehiclesResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public searchSalesVehiclesResponseBody()
    {
    }

    public searchSalesVehiclesResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getAllInvoicesRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getAllInvoices", Namespace = "http://www.fmade.at")]
    public getAllInvoicesRequestBody Body;

    public getAllInvoicesRequest()
    {
    }

    public getAllInvoicesRequest(getAllInvoicesRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getAllInvoicesRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
    public int arg0;

    [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
    public int arg1;

    public getAllInvoicesRequestBody()
    {
    }

    public getAllInvoicesRequestBody(int arg0, int arg1)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getAllInvoicesResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getAllInvoicesResponse", Namespace = "http://www.fmade.at")]
    public getAllInvoicesResponseBody Body;

    public getAllInvoicesResponse()
    {
    }

    public getAllInvoicesResponse(getAllInvoicesResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getAllInvoicesResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public getAllInvoicesResponseBody()
    {
    }

    public getAllInvoicesResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getNewInvoicesCount", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getNewInvoicesCountRequest
{

    public getNewInvoicesCountRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getNewInvoicesCountResponse", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getNewInvoicesCountResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "")]
    public int @return;

    public getNewInvoicesCountResponse()
    {
    }

    public getNewInvoicesCountResponse(int @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getNewInvoiceRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getNewInvoice", Namespace = "http://www.fmade.at")]
    public getNewInvoiceRequestBody Body;

    public getNewInvoiceRequest()
    {
    }

    public getNewInvoiceRequest(getNewInvoiceRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getNewInvoiceRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
    public int arg0;

    [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
    public int arg1;

    public getNewInvoiceRequestBody()
    {
    }

    public getNewInvoiceRequestBody(int arg0, int arg1)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getNewInvoiceResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getNewInvoiceResponse", Namespace = "http://www.fmade.at")]
    public getNewInvoiceResponseBody Body;

    public getNewInvoiceResponse()
    {
    }

    public getNewInvoiceResponse(getNewInvoiceResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getNewInvoiceResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public getNewInvoiceResponseBody()
    {
    }

    public getNewInvoiceResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class gotNewInvoicesRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "gotNewInvoices", Namespace = "http://www.fmade.at")]
    public gotNewInvoicesRequestBody Body;

    public gotNewInvoicesRequest()
    {
    }

    public gotNewInvoicesRequest(gotNewInvoicesRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute()]
public partial class gotNewInvoicesRequestBody
{

    public gotNewInvoicesRequestBody()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class gotNewInvoicesResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "gotNewInvoicesResponse", Namespace = "http://www.fmade.at")]
    public gotNewInvoicesResponseBody Body;

    public gotNewInvoicesResponse()
    {
    }

    public gotNewInvoicesResponse(gotNewInvoicesResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class gotNewInvoicesResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public gotNewInvoicesResponseBody()
    {
    }

    public gotNewInvoicesResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class createNewCustomerRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "createNewCustomer", Namespace = "http://www.fmade.at")]
    public createNewCustomerRequestBody Body;

    public createNewCustomerRequest()
    {
    }

    public createNewCustomerRequest(createNewCustomerRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class createNewCustomerRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public createNewCustomerRequestBody()
    {
    }

    public createNewCustomerRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class createNewCustomerResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "createNewCustomerResponse", Namespace = "http://www.fmade.at")]
    public createNewCustomerResponseBody Body;

    public createNewCustomerResponse()
    {
    }

    public createNewCustomerResponse(createNewCustomerResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class createNewCustomerResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public createNewCustomerResponseBody()
    {
    }

    public createNewCustomerResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class createNewCustomerVehicleRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "createNewCustomerVehicle", Namespace = "http://www.fmade.at")]
    public createNewCustomerVehicleRequestBody Body;

    public createNewCustomerVehicleRequest()
    {
    }

    public createNewCustomerVehicleRequest(createNewCustomerVehicleRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class createNewCustomerVehicleRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public createNewCustomerVehicleRequestBody()
    {
    }

    public createNewCustomerVehicleRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class createNewCustomerVehicleResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "createNewCustomerVehicleResponse", Namespace = "http://www.fmade.at")]
    public createNewCustomerVehicleResponseBody Body;

    public createNewCustomerVehicleResponse()
    {
    }

    public createNewCustomerVehicleResponse(createNewCustomerVehicleResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class createNewCustomerVehicleResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public createNewCustomerVehicleResponseBody()
    {
    }

    public createNewCustomerVehicleResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class createNewContactPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "createNewContactPerson", Namespace = "http://www.fmade.at")]
    public createNewContactPersonRequestBody Body;

    public createNewContactPersonRequest()
    {
    }

    public createNewContactPersonRequest(createNewContactPersonRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class createNewContactPersonRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public createNewContactPersonRequestBody()
    {
    }

    public createNewContactPersonRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class createNewContactPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "createNewContactPersonResponse", Namespace = "http://www.fmade.at")]
    public createNewContactPersonResponseBody Body;

    public createNewContactPersonResponse()
    {
    }

    public createNewContactPersonResponse(createNewContactPersonResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class createNewContactPersonResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public createNewContactPersonResponseBody()
    {
    }

    public createNewContactPersonResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getSalesVehicleDetailRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getSalesVehicleDetail", Namespace = "http://www.fmade.at")]
    public getSalesVehicleDetailRequestBody Body;

    public getSalesVehicleDetailRequest()
    {
    }

    public getSalesVehicleDetailRequest(getSalesVehicleDetailRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getSalesVehicleDetailRequestBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string arg0;

    public getSalesVehicleDetailRequestBody()
    {
    }

    public getSalesVehicleDetailRequestBody(string arg0)
    {
        this.arg0 = arg0;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
public partial class getSalesVehicleDetailResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Name = "getSalesVehicleDetailResponse", Namespace = "http://www.fmade.at")]
    public getSalesVehicleDetailResponseBody Body;

    public getSalesVehicleDetailResponse()
    {
    }

    public getSalesVehicleDetailResponse(getSalesVehicleDetailResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.Runtime.Serialization.DataContractAttribute(Namespace = "")]
public partial class getSalesVehicleDetailResponseBody
{

    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
    public string @return;

    public getSalesVehicleDetailResponseBody()
    {
    }

    public getSalesVehicleDetailResponseBody(string @return)
    {
        this.@return = @return;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getAllInvoiceCount", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getAllInvoiceCountRequest
{

    public getAllInvoiceCountRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "getAllInvoiceCountResponse", WrapperNamespace = "http://www.fmade.at", IsWrapped = true)]
public partial class getAllInvoiceCountResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "")]
    public int @return;

    public getAllInvoiceCountResponse()
    {
    }

    public getAllInvoiceCountResponse(int @return)
    {
        this.@return = @return;
    }
}