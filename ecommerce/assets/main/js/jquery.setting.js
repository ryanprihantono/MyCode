	/* Nivo Slider */
	
	$(window).load(function() {
		$('#slider').nivoSlider();
	});
	
	/* Cycle */

	$('.our-news').cycle({ 
		fx:     'scrollUp', 
		speed:   300, 
		timeout: 3000, 
		next:   '#our-news', 
		pause:   1 
	});
    
	$('.sidebar-scroll').cycle({ 
		fx:     'scrollUp', 
		speed:   300, 
		timeout: 3000, 
		next:   '.sidebar-scrioll', 
		pause:   1 
	});