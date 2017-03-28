var YellowPages = function () {
    var self = this;

    // API shortforms
    var orgRegisterApi = 'https://organization-registration-service.mybluemix.net/api/organization-register';
    
    // Tab definitions
    self.organizationTab = (function () {
        var orgList = ko.observableArray();
        var loadOrganizationList = function () {
            var onSuccess = function (organizations) {
                console.log(organizations);
                organizations.forEach(function (organization) {
                    self.organizationTab.orgList.push(organization);
                });
            };
            var onError = function () {
                alert('Could not retrieve organizations.');
            };

            xhrGet(orgRegisterApi + '/getAllOrgs', onSuccess, onError);
        };

        var clearAddOrgModal = function () {
            self.organizationTab.addOrgModal.name('');
            self.organizationTab.addOrgModal.description('');
        };

        var displayAddOrgModal = function () {
            clearAddOrgModal();
            $('#addOrgModal').modal('show');
        };

        var addOrgModal = (function () {
            var name = ko.observable('');
            var description = ko.observable('');
            var closeAddOrgModal = function () {
                $('#addOrgModal').modal('hide');
            };
            var addOrg = function (name, description) {
                var params = {
                    name: name,
                    description, description
                };
                var onSuccess = function () {
                    self.organizationTab.loadOrganizationList();
                    alert('Successfully added organization!');
                    self.organizationTab.addOrgModal.closeAddOrgModal();
                };
                var onError = function () {
                    alert('Failed to add organization');
                };

                console.log(params);
                xhrPost(orgRegisterApi, params, onSuccess, onError);
            };

            var exports = {};
            exports.name = name;
            exports.description = description;
            exports.closeAddOrgModal = closeAddOrgModal;
            exports.addOrg = addOrg;
            return exports;
        }());

        var exports = {};
        exports.orgList = orgList;
        exports.loadOrganizationList = loadOrganizationList;
        exports.displayAddOrgModal = displayAddOrgModal;
        exports.addOrgModal = addOrgModal;
        return exports;
    }());

    self.serviceSearchTab = (function () {
        // Add Service Search tab functions here



        var exports = {};
        return exports;
    }());

    self.organizationTab.loadOrganizationList(); // Immediately load the organization list

    return self;
};

ko.applyBindings(new YellowPages());