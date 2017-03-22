using Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    
    public class SIT2_Item
    {

        
        private string _project;

        [Name("Project")]
        public string Project
        {
            get { return _project; }
            set { _project = value; }
        }

        private string _requesterName;

        [Name("Requester Name")]
        public string RequesterName
        {
            get { return _requesterName; }
            set { _requesterName = value; }
        }

        
        private string _bussinessRelease;

        [Name("Bussiness Release")]
        public string BussinessRelease
        {
            get { return _bussinessRelease; }
            set { _bussinessRelease = value; }
        }


        private string _deploymentWindow;

        [Name("Deployment Window")]
        public string DeploymentWindow
        {
            get { return _deploymentWindow; }
            set { _deploymentWindow = value; }
        }


        private string _deploymentType;

        [Name("Deployment Type")]
        public string DeploymentType
        {
            get { return _deploymentType; }
            set { _deploymentType = value; }
        }

        private string _deploymentReason;

        [Name("Deployment Reason")]
        public string DeploymentReason
        {
            get { return _deploymentReason; }
            set { _deploymentReason = value; }
        }

        private string _application;

        [Name("Application")]
        public string Application
        {
            get { return _application; }
            set { _application = value; }
        }



        void Test()
        {
            Type t;
            
        }

    }
}
