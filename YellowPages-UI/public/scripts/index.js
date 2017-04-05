var YellowPages = function () {
    var self = this;

    // API shortforms
    var orgRegisterApi = 'https://organization-registration-service.mybluemix.net/api/organization-register';
	var serviceRegisterApi = 'https://service-registration-service.mybluemix.net/api/service';
    var serviceSearchApi = 'https://service-search-service.mybluemix.net/api/search';
	var invokeServiceApi = 'https://service-invocation-service.mybluemix.net/api/invoke';

    // Tab definitions
    self.organizationTab = (function () {
        var orgList = ko.observableArray();

        var loadServicesForOrg = function (org) {
            var onSuccess = function (services) {
                services.forEach(function (service) {
                    service.orgId = service.organizationId;
                    service.organizationId = undefined;
                    service.inputs = service.input;
                    service.input = undefined;
                    org.serviceList.push(service);
                });
            };
            var onError = function () {
                var service = {
                    "orgId": "be4d234e-efde-49a1-bd53-ab2baf0d0722",
                    "name": "Example Service 2",
                    "description": "A test service 2",
                    "tags": [
                      "tag 1",
                      "tag 2"
                    ],
                    "inputs": [
                      {
                          "name": "start",
                          "type": "Date"
                      },
                      {
                          "name": "end",
                          "type": "Date"
                      }
                    ]
                }
                org.serviceList.push(service);
                //alert('Could not load services');
            };
            xhrGet(serviceRegisterApi + '?organizationId=' + org.id, onSuccess, onError);
        };
        var loadOrganizationList = function () {
            var onSuccess = function (organizations) {
                organizations.sort(function (a, b) {
                    var nameA = a.name.toUpperCase(); 
                    var nameB = b.name.toUpperCase(); 
                    if (nameA < nameB) {
                        return -1;
                    }
                    if (nameA > nameB) {
                        return 1;
                    }

                    return 0;
                });
                
                self.organizationTab.orgList.removeAll();
                organizations.forEach(function (organization) {
                    organization.serviceList = ko.observableArray([]);
                    self.organizationTab.orgList.push(organization);
                    self.organizationTab.loadServicesForOrg(organization);
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

            var addOrgModal = {};
            addOrgModal.name = name;
            addOrgModal.description = description;
            addOrgModal.closeAddOrgModal = closeAddOrgModal;
            addOrgModal.addOrg = addOrg;
            return addOrgModal;
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
			
			var editOrgModal = {};
			editOrgModal.id = id;
			editOrgModal.name = name;
			editOrgModal.description = description;
			editOrgModal.closeEditOrgModal = closeEditOrgModal;
			editOrgModal.editOrg = editOrg;
			return editOrgModal;
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
			var removeOrgModal = {};
			removeOrgModal.id = id;
			removeOrgModal.name = name;
			removeOrgModal.closeRemoveOrgModal = closeRemoveOrgModal;
			removeOrgModal.removeOrg = removeOrg;
			return removeOrgModal;
		}());
		
		var clearAddServiceModal = function () {
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
			var orgId = ko.observable('');
			var name = ko.observable('');
			var description = ko.observable('');
			var tags = ko.observableArray([]);
			var inputs = ko.observableArray([]);
			
			var closeAddServiceModal = function () {
				$('#addServiceModal').modal('hide');
			};
			var addService = function (orgId, name, description, tags, inputs) {
			    tags = tags.map(function (tag) {
			        return '' + tag.tag();
			    }).filter(function (tag) {
			        return !!tag;
			    });

			    inputs = inputs.map(function (input) {
			        input.name = '' + input.name();
			        input.type = '' + input.type();
			        return input;
			    }).filter(function (input) {
			        return !!input.name && !!input.type;
			    });

			    var params = {
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
				xhrPostJson(serviceRegisterApi, params, onSuccess, onError);
			};

			var addTag = function () {
			    self.organizationTab.addServiceModal.tags.push({
                    tag: ko.observable('')
			    });
			};

			var removeTag = function (index) {
			    self.organizationTab.addServiceModal.tags.splice(index, 1);
			};
			
			var addInput = function () {
			    self.organizationTab.addServiceModal.inputs.push({
			        name: ko.observable(''),
                    type: ko.observable('')
			    });
			};

			var removeInput = function (index) {
			    self.organizationTab.addServiceModal.inputs.splice(index, 1);
			};

			var addServiceModal = {};
			addServiceModal.orgId = orgId;
			addServiceModal.name = name;
			addServiceModal.description = description;
			addServiceModal.tags = tags;
			addServiceModal.inputs = inputs;
			addServiceModal.closeAddServiceModal = closeAddServiceModal;
			addServiceModal.addService = addService;
			addServiceModal.addTag = addTag;
			addServiceModal.removeTag = removeTag;
			addServiceModal.addInput = addInput;
			addServiceModal.removeInput = removeInput;
			return addServiceModal;
		}());

		var setEditServiceModal = function (service) {
		    self.organizationTab.editServiceModal.id(service.id);
		    self.organizationTab.editServiceModal.orgId(service.orgId);
		    self.organizationTab.editServiceModal.name(service.name);
		    self.organizationTab.editServiceModal.description(service.description);
		    self.organizationTab.editServiceModal.tags.removeAll();
		    self.organizationTab.editServiceModal.inputs.removeAll();
		    service.tags.forEach(function (tag) {
		        self.organizationTab.editServiceModal.tags.push({ tag: ko.observable(tag) });
		    });
		    service.inputs.forEach(function (input) {
		        self.organizationTab.editServiceModal.inputs.push({ name: ko.observable(input.name), type: ko.observable(input.type) });
		    });
		};

		var displayEditServiceModal = function (service) {
		    self.organizationTab.setEditServiceModal(service);
		    $('#editServiceModal').modal('show');
		};

		var editServiceModal = (function () {
		    var id = ko.observable('');
		    var orgId = ko.observable('');
		    var name = ko.observable('');
		    var description = ko.observable('');
		    var tags = ko.observableArray([]);
		    var inputs = ko.observableArray([]);

		    var closeEditServiceModal = function () {
		        $('#editServiceModal').modal('hide');
		    };

		    var editService = function (id, orgId, name, description, tags, inputs) {
		        tags = tags.map(function (tag) {
		            return '' + tag.tag();
		        }).filter(function (tag) {
		            return !!tag;
		        });

		        inputs = inputs.map(function (input) {
		            input.name = '' + input.name();
		            input.type = '' + input.type();
		            return input;
		        }).filter(function (input) {
		            return !!input.name && !!input.type;
		        });

		        var params = {
		            organizationId: orgId,
		            name: name,
		            description: description,
		            tags: tags,
                    input: inputs
		        };
		        var onSuccess = function () {
		            alert('Successfully editted service!');
		            self.organizationTab.editServiceModal.closeEditServiceModal();
		            self.organizationTab.loadOrganizationList();
		        };
		        var onError = function () {
		            alert('Could not edit service.');
		        };
		        xhrPutJson(serviceRegisterApi + '/' + id, params, onSuccess, onError);
		    };

		    var addTag = function () {
		        self.organizationTab.editServiceModal.tags.push({
		            tag: ko.observable('')
		        });
		    };

		    var removeTag = function (index) {
		        self.organizationTab.editServiceModal.tags.splice(index, 1);
		    };

		    var addInput = function () {
		        self.organizationTab.editServiceModal.inputs.push({
		            name: ko.observable(''),
		            type: ko.observable('')
		        });
		    };

		    var removeInput = function (index) {
		        self.organizationTab.editServiceModal.inputs.splice(index, 1);
		    };

		    var editServiceModal = {};
		    editServiceModal.id = id;
		    editServiceModal.orgId = orgId;
		    editServiceModal.name = name;
		    editServiceModal.description = description;
		    editServiceModal.tags = tags;
		    editServiceModal.inputs = inputs;
		    editServiceModal.closeEditServiceModal = closeEditServiceModal;
		    editServiceModal.addTag = addTag;
		    editServiceModal.removeTag = removeTag;
		    editServiceModal.addInput = addInput;
		    editServiceModal.removeInput = removeInput;
		    editServiceModal.editService = editService;
		    return editServiceModal;
		}());

		var setRemoveServiceModal = function (service) {
		    self.organizationTab.removeServiceModal.id(service.id);
		    self.organizationTab.removeServiceModal.name(service.name);
		};

		var displayRemoveServiceModal = function (service) {
		    self.organizationTab.setRemoveServiceModal(service);
		    $('#removeServiceModal').modal('show');
		};

		var removeServiceModal = (function () {
		    var id = ko.observable('');
		    var name = ko.observable('');

		    var closeRemoveServiceModal = function () {
		        $('#removeServiceModal').modal('hide');
		    };

		    var removeService = function (id) {
		        var onSuccess = function () {
		            alert('Successfully removed service!');
		            self.organizationTab.removeServiceModal.closeRemoveServiceModal();
		            self.organizationTab.loadOrganizationList();
		        };
		        var onError = function () {
		            alert('Failed to remove service.');
		        };
		        xhrDelete(serviceRegisterApi + '/' + id, onSuccess, onError);
		    };

		    var removeServiceModal = {};
		    removeServiceModal.id = id;
		    removeServiceModal.name = name;
		    removeServiceModal.closeRemoveServiceModal = closeRemoveServiceModal;
		    removeServiceModal.removeService = removeService;
		    return removeServiceModal;
		}());

		var organizationTab = {};
		organizationTab.orgList = orgList;
		organizationTab.loadOrganizationList = loadOrganizationList;
		organizationTab.loadServicesForOrg = loadServicesForOrg;
		organizationTab.displayAddOrgModal = displayAddOrgModal;
		organizationTab.addOrgModal = addOrgModal;
		organizationTab.displayEditOrgModal = displayEditOrgModal;
		organizationTab.editOrgModal = editOrgModal;
		organizationTab.displayRemoveOrgModal = displayRemoveOrgModal;
		organizationTab.removeOrgModal = removeOrgModal;
		organizationTab.displayAddServiceModal = displayAddServiceModal;
		organizationTab.clearAddServiceModal = clearAddServiceModal;
		organizationTab.addServiceModal = addServiceModal;
		organizationTab.displayEditServiceModal = displayEditServiceModal;
		organizationTab.setEditServiceModal = setEditServiceModal;
		organizationTab.editServiceModal = editServiceModal;
		organizationTab.displayRemoveServiceModal = displayRemoveServiceModal;
		organizationTab.setRemoveServiceModal = setRemoveServiceModal;
		organizationTab.removeServiceModal = removeServiceModal;

		return organizationTab;
    }());

    self.serviceSearchTab = (function () {
		var keywords = ko.observable('');
		var serviceResults = ko.observableArray();
		var hasSearched = ko.observable(false);

		var searchForService = (function (keywords){
			var params = {
				keywords: keywords
			};
			var onSuccess = function (services) {
				self.serviceSearchTab.serviceResults.removeAll();
				services.forEach(function (service) {
                    self.serviceSearchTab.serviceResults.push(service);
                });
				self.serviceSearchTab.hasSearched(true);
			};
			var onError = function () {
				alert('Failed to search for services.');
			};
			xhrPostJson(serviceSearchApi, params, onSuccess, onError);
		});

		var displayServiceInvokeModal = function (serv) {
			self.serviceSearchTab.invokeServiceModal.id(serv.id);
			self.serviceSearchTab.invokeServiceModal.name(serv.name);
			self.serviceSearchTab.invokeServiceModal.description(serv.description);
			self.serviceSearchTab.invokeServiceModal.inputs(serv.input);
			$('#invokeServiceModal').modal('show');
		};

		var invokeServiceModal = (function () {
		    var id = ko.observable('');
			var name = ko.observable('');
			var description = ko.observable('');
		    var inputs = ko.observableArray([]);
			var result = ko.observable('');

		    var closeInvokeServiceModal = function () {
		        $('#invokeServiceModal').modal('hide');
		    };

		    var invokeService = function (id, input) {
				var params = {
					serviceId: id,
					input: input
				};
		        var onSuccess = function (response) {
					self.serviceSearchTab.invokeServiceModal.result(response);
		        };
		        var onError = function () {
		            alert('Failed to invoke service.');
		        };
		        xhrPostJson(invokeServiceApi, params, onSuccess, onError);
		    };

		    var invokeServiceModal = {};
		    invokeServiceModal.id = id;
			invokeServiceModal.name = name;
			invokeServiceModal.description = description;
			invokeServiceModal.inputs = inputs;
			invokeServiceModal.result = result;
		    invokeServiceModal.closeInvokeServiceModal = closeInvokeServiceModal;
			invokeServiceModal.invokeService = invokeService;
		    return invokeServiceModal;
		}());

        var serviceSearchTab = {};
		serviceSearchTab.keywords = keywords;
		serviceSearchTab.searchForService = searchForService;
		serviceSearchTab.serviceResults = serviceResults;
		serviceSearchTab.hasSearched = hasSearched;
		serviceSearchTab.displayServiceInvokeModal = displayServiceInvokeModal;
		serviceSearchTab.invokeServiceModal = invokeServiceModal;
        return serviceSearchTab;
    }());

    self.organizationTab.loadOrganizationList(); // Immediately load the organization list

    return self;
};

ko.applyBindings(new YellowPages());