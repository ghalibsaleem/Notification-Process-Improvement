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

        private string _targetEnv;

        [Name("Target Environment")]
        public string TargetEnvironment
        {
            get { return _targetEnv; }
            set { _targetEnv = value; }
        }

        private string _affectedEnv;

        [Name("Affected Environment")]
        public string AffectedEnvironment
        {
            get { return _affectedEnv; }
            set { _affectedEnv = value; }
        }


        private string _depItemType;

        [Name("Deployment Item Type")]
        public string DeploymentItemType
        {
            get { return _depItemType; }
            set { _depItemType = value; }
        }


        private string _buildNos;

        [Name("Build Numbers")]
        public string BuildNumbers
        {
            get { return _buildNos; }
            set { _buildNos = value; }
        }

        private string _releaseNoteLocation;

        [Name("Release Note Location")]
        public string ReleaseNoteLocation
        {
            get { return _releaseNoteLocation; }
            set { _releaseNoteLocation = value; }
        }


        private string _testRepotLocation;

        [Name("Test Report Location")]
        public string TestReportLocation
        {
            get { return _testRepotLocation; }
            set { _testRepotLocation = value; }
        }

        private string _testedInEnv;

        [Name("Tested In Environment")]
        public string TestedInEnv
        {
            get { return _testedInEnv; }
            set { _testedInEnv = value; }
        }

        private string _testersInvolved;

        [Name("Testers Involved")]
        public string TestersInvolved
        {
            get { return _testersInvolved; }
            set { _testersInvolved = value; }
        }


        private string _deploymentGuideline;

        [Name("Deployment Guidelines")]
        public string DeploymentGuidelines
        {
            get { return _deploymentGuideline; }
            set { _deploymentGuideline = value; }
        }

    }
}
