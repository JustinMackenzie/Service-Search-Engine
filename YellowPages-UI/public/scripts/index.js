var YellowPages = function () {
    var self = this;

    // API shortforms
    var orgRegisterApi = 'https://organization-registration-service.mybluemix.net/api/organization-register';
	var serviceRegisterApi = 'https://service-registration-service.mybluemix.net/api/service';
    
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
                    alert('Failed to add organization.');
                };

                xhrPost(orgRegisterApi, params, onSuccess, onError);
            };
			

            var exports = {};
            exports.name = name;
            exports.description = description;
            exports.closeAddOrgModal = closeAddOrgModal;
            exports.addOrg = addOrg;
            return exports;
        }());
		
		var displayEditOrgModal = function (org) {
			self.organizationTab.editOrgModal.id(org.id);
			self.organizationTab.editOrgModal.name(org.name);
			self.organizationTab.editOrgModal.description(org.description);
			$('#editOrgModal').modal('show');
		};
		
		var editOrgModal = (function () {
			var id = ko.observable('');
			var name = ko.observable('');
            var description = ko.observable('');
            var closeEditOrgModal = function () {
                $('#editOrgModal').modal('hide');
            };
			var editOrg = function (id, name, description) {
				var params = {
					id: id,
					name: name,
					description: description
				};
				var onSuccess = function () {
                    self.organizationTab.loadOrganizationList();
                    alert('Successfully editted organization!');
                    self.organizationTab.editOrgModal.closeEditOrgModal();
                };
                var onError = function () {
                    alert('Failed to edit organization.');
                };
				xhrPut(orgRegisterApi, params, onSuccess, onError);
			};
			
			var exports = {};
			exports.id = id;
            exports.name = name;
            exports.description = description;
            exports.closeEditOrgModal = closeEditOrgModal;
            exports.editOrg = editOrg;
            return exports;
		}());
		
		var displayRemoveOrgModal = function (org) {
			self.organizationTab.removeOrgModal.id(org.id);
			self.organizationTab.removeOrgModal.name(org.name);
			$('#removeOrgModal').modal('show');
		};
		
		var removeOrgModal = (function () {
			var id = ko.observable('');
			var name = ko.observable('');
			var closeRemoveOrgModal = function () {
				$('#removeOrgModal').modal('hide');
			};
			var removeOrg = function (id) {
				var onSuccess = function () {
					self.organizationTab.loadOrganizationList();
                    alert('Successfully removed organization!');
                    self.organizationTab.removeOrgModal.closeRemoveOrgModal();
				};
				var onError = function () {
					alert('Failed to remove organization.');
				};
				xhrDelete(orgRegisterApi + '?id=' + id, onSuccess, onError);
			};
			var exports = {};
			exports.id = id;
			exports.name = name;
			exports.closeRemoveOrgModal = closeRemoveOrgModal;
			exports.removeOrg = removeOrg;
			return exports;
		}());
		
		var clearAddServiceModal = function () {
			self.organizationTab.addServiceModal.id('');
			self.organizationTab.addServiceModal.orgId('');
			self.organizationTab.addServiceModal.name('');
			self.organizationTab.addServiceModal.description('');
			self.organizationTab.addServiceModal.tags.removeAll();
			self.organizationTab.addServiceModal.inputs.removeAll();
		};
		
		var displayAddServiceModal = function (org) {
			self.organizationTab.clearAddServiceModal();
			self.organizationTab.addServiceModal.orgId(org.id);
			$('#addServiceModal').modal('show');
		};
		
		var addServiceModal = (function () {
			var id = ko.observable('');
			var orgId = ko.observable('');
			var name = ko.observable('');
			var description = ko.observable('');
			var tags = ko.observableArray([]);
			var inputs = ko.observableArray([]);
			
			var closeAddServiceModal = function () {
				$('#addServiceModal').modal('hide');
			};
			var addService = function (id, orgId, name, description, tags, inputs) {
				var params = {
					id: id,
					organizationId: orgId,
					name: name,
					description: description,
					tags: tags,
					input: inputs
				};
				var onSuccess = function () {
					self.organizationTab.loadOrganizationList();
					alert('Successfully added service!');
					self.organizationTab.addServiceModal.closeAddServiceModal();
				};
				var onError = function () {
					alert('Failed to add service.');
				};
				//xhrPost(serviceRegisterApi, params, onSuccess, onError);
			};
			
			var exports = {};
			exports.id = id;
			exports.orgId = orgId;
			exports.name = name;
			exports.description = description;
			exports.tags = tags;
			exports.inputs = inputs;
			exports.closeAddServiceModal = closeAddServiceModal;
			exports.addService = addService;
			return exports;
		}());

        var exports = {};
        exports.orgList = orgList;
        exports.loadOrganizationList = loadOrganizationList;
        exports.displayAddOrgModal = displayAddOrgModal;
        exports.addOrgModal = addOrgModal;
		exports.displayEditOrgModal = displayEditOrgModal;
		exports.editOrgModal = editOrgModal;
		exports.displayRemoveOrgModal = displayRemoveOrgModal;
		exports.removeOrgModal = removeOrgModal;
		exports.displayAddServiceModal = displayAddServiceModal;
		exports.clearAddServiceModal = clearAddServiceModal;
		exports.addServiceModal = addServiceModal;
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