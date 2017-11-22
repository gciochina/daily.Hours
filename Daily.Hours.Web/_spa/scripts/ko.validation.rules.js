ko.validation.rules['minCheck'] = {
  validator: function(val, min) {
      val=val.replace(/\,/g,'');
      return ko.validation.utils.isEmptyVal(val) || val >= min;
    },
 message: 'Please enter a value greater than or equal to {0}.'
};

ko.validation.rules['maxCheck'] = {
	validator: function (val, max) {
		val = val.replace(/\,/g, '');
		return ko.validation.utils.isEmptyVal(val) || val >= max;
	},
	message: 'Please enter a value less than or equal to {0}.'
};