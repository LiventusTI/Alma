//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoAlmaInicio.ServiceReferenceDescifrado {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceDescifrado.IWSDataController")]
    public interface IWSDataController {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWSDataController/CargarDataController", ReplyAction="http://tempuri.org/IWSDataController/CargarDataControllerResponse")]
        int CargarDataController(int idServicio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWSDataController/CargarDataController", ReplyAction="http://tempuri.org/IWSDataController/CargarDataControllerResponse")]
        System.Threading.Tasks.Task<int> CargarDataControllerAsync(int idServicio);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWSDataControllerChannel : ProyectoAlmaInicio.ServiceReferenceDescifrado.IWSDataController, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSDataControllerClient : System.ServiceModel.ClientBase<ProyectoAlmaInicio.ServiceReferenceDescifrado.IWSDataController>, ProyectoAlmaInicio.ServiceReferenceDescifrado.IWSDataController {
        
        public WSDataControllerClient() {
        }
        
        public WSDataControllerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSDataControllerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSDataControllerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSDataControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int CargarDataController(int idServicio) {
            return base.Channel.CargarDataController(idServicio);
        }
        
        public System.Threading.Tasks.Task<int> CargarDataControllerAsync(int idServicio) {
            return base.Channel.CargarDataControllerAsync(idServicio);
        }
    }
}
