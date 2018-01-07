(function($){
	$.fn.sayAccordion = function(options) {
		var defaults = {
			 aBtn: 'accordion-title',
			 aContent: 'accordion-content',
			 active: 'open'
		};
		var options = $.extend({}, defaults, options);
		$(document).ready(function() {
			$('.'+options.aContent).hide();
			$('.'+options.aBtn).each(function() {
				if($(this).hasClass(options.active)){
					$(this).siblings('.'+options.aContent).addClass(options.active).slideDown();
				}						  						  
			});
	   });
	   $('.'+options.aBtn).click(function() {
		   var obj = $(this);
		   var slide = true;
		   if(obj.hasClass('open')){
				slide = false;
		   }
		   $('.'+options.aContent).slideUp().removeClass(options.active);
		   $('.'+options.aBtn).removeClass(options.active);
		   if(slide){
			   obj.addClass(options.active);
			   obj.siblings('.'+options.aContent).addClass(options.active).slideDown();
		   }
		   return false;					   
	   });
	}
})(jQuery);