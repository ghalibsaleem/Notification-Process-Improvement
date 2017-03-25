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

        private string _id;

        [Sno(0)]
        [Name("Id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }



        private string _project;

        [Sno(1)]
        [Name("Project")]
        public string Project
        {
            get { return _project; }
            set { _project = value; }
        }

        private string _requesterName;

        [Sno(2)]
        [Name("Requester Name")]
        public string RequesterName
        {
            get { return _requesterName; }
            set { _requesterName = value; }
        }


        
        private string _businessRelease;

        [Sno(3)]
        [Name("Business Release")]
        public string BusinessRelease
        {
            get { return _businessRelease; }
            set { _businessRelease = value; }
        }


        private string _deploymentWindow;

        [Sno(4)]
        [Name("Deployment Window")]
        public string DeploymentWindow
        {
            get { return _deploymentWindow; }
            set { _deploymentWindow = value; }
        }


        private string _deploymentType;

        [Sno(5)]
        [Name("Deployment type")]
        public string DeploymentType
        {
            get { return _deploymentType; }
            set { _deploymentType = value; }
        }

        private string _deploymentReason;

        [Sno(6)]
        [Name("Deployment Reason")]
        public string DeploymentReason
        {
            get { return _deploymentReason; }
            set { _deploymentReason = value; }
        }

        private string _application;

        [Sno(7)]
        [Name("Application")]
        public string Application
        {
            get { return _application; }
            set { _application = value; }
        }

        private string _targetEnv;

        [Sno(8)]
        [Name("Target Environment")]
        public string TargetEnvironment
        {
            get { return _targetEnv; }
            set { _targetEnv = value; }
        }

        private string _affectedTech;

        [Sno(9)]
        [Name("Affected Technologies")]
        public string AffectedTechnologies
        {
            get { return _affectedTech; }
            set { _affectedTech = value; }
        }


        private string _depItemType;

        [Sno(10)]
        [Name("Deployment Item types")]
        public string DeploymentItemType
        {
            get { return _depItemType; }
            set { _depItemType = value; }
        }


        private string _buildNos;

        [Sno(11)]
        [Name("Build numbers")]
        public string BuildNumbers
        {
            get { return _buildNos; }
            set { _buildNos = value; }
        }

        private string _releaseNoteLocation;

        [Sno(12)]
        [Name("Release Note Location")]
        public string ReleaseNoteLocation
        {
            get { return _releaseNoteLocation; }
            set { _releaseNoteLocation = value; }
        }


        private string _testRepotLocation;

        [Sno(13)]
        [Name("Test Report Location")]
        public string TestReportLocation
        {
            get { return _testRepotLocation; }
            set { _testRepotLocation = value; }
        }

        private string _testedInEnv;

        [Sno(14)]
        [Name("Tested in Environment")]
        public string TestedInEnv
        {
            get { return _testedInEnv; }
            set { _testedInEnv = value; }
        }

        private string _testersInvolved;

        [Sno(15)]
        [Name("Testers involved")]
        public string TestersInvolved
        {
            get { return _testersInvolved; }
            set { _testersInvolved = value; }
        }


        private string _deploymentGuideline;

        [Sno(16)]
        [Name("Deployment Guideline")]
        public string DeploymentGuidelines
        {
            get { return _deploymentGuideline; }
            set { _deploymentGuideline = value; }
        }

        private string _isMannualConfig;

        [Sno(17)]
        [Name("Deployment Guideline has manual Configuration Changes?")]
        public string IsMannualConfig
        {
            get { return _isMannualConfig; }
            set { _isMannualConfig = value; }
        }


        private string _hasRollbackInstruct;

        [Sno(18)]
        [Name("Deployment Guideline has Rollback instructions")]
        public string HasRollbackInstruct
        {
            get { return _hasRollbackInstruct; }
            set { _hasRollbackInstruct = value; }
        }


        private string _locationOfDep;

        [Sno(19)]
        [Name("Locations of the deployment items")]
        public string LocationOfDep
        {
            get { return _locationOfDep; }
            set { _locationOfDep = value; }
        }


        private string _dependencies;

        [Sno(20)]
        [Name("Dependencies")]
        public string Dependencies
        {
            get { return _dependencies; }
            set { _dependencies = value; }
        }


        private string _projectApproval;

        [Sno(21)]
        [Name("Project's approval")]
        public string ProjectApproval
        {
            get { return _projectApproval; }
            set { _projectApproval = value; }
        }


        private string _shortDeployment ;

        [Sno(22)]
        [Name("Short deployment request message")]
        public string ShortDeployment
        {
            get { return _shortDeployment; }
            set { _shortDeployment = value; }
        }


        private string _managerApproval;

        [Sno(23)]
        [Name("Release Manager's approval")]
        public string ManagerApproval
        {
            get { return _managerApproval; }
            set { _managerApproval = value; }
        }

        private string _comments;

        [Sno(24)]
        [Name("Comments")]
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        private string _deploymentStatus;

        [Sno(25)]
        [Name("Deployment status")]
        public string DeploymentStatus
        {
            get { return _deploymentStatus; }
            set { _deploymentStatus = value; }
        }

        private string _teamDeployDate;

        [Sno(26)]
        [Name("Deployment Team deployed date and time")]
        public string TeamDeployDate
        {
            get { return _teamDeployDate; }
            set { _teamDeployDate = value; }
        }

        private string _deploymentOutcome;

        [Sno(27)]
        [Name("Deployment Outcome")]
        public string DeploymentOutcome
        {
            get { return _deploymentOutcome; }
            set { _deploymentOutcome = value; }
        }

        private string _passedSmokeTest;

        [Sno(28)]
        [Name("Deployment passed Smoke test")]
        public string PassedSmokeTest
        {
            get { return _passedSmokeTest; }
            set { _passedSmokeTest = value; }
        }

    }
}
