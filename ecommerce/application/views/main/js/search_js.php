<script type="text/javascript">
	$(function(){
		var availableTags = new Array();
		$("#txtsearch").keyup(function(){
			if(this.value != ""){
				$.getJSON('<?php echo base_url().'main/autosearch'; ?>/'+this.value,function(data){
					$("#txtsearch").autocomplete({
						source: data.tags,
						select:function(event, ui){
							window.location.replace('<?php echo base_url().'main/dosearch'; ?>/'+ui.item.value);
						}
					});
				});
			}
		});
		
	});
</script>