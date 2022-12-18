
var idNum = 0, data = 'elastic';
$('body').on('keyup', 'textarea[data^="'+data+'"]', function(){
if($(this).attr('data')==''+data+''){$(this).attr({style:'overflow:hidden;'+$(this).attr('style')+'',data:''+$(this).attr('data')+''+idNum+''});idNum++;}
tData = $(this).attr('data');
if($('div[data="'+tData.replace(''+data+'','clone')+'"]').size()==0){
attr = 'style="display:none;padding:'+$(this).css('padding')+';width:'+$(this).css('width')+';min-height:'+$(this).css('height')+';font-size:'+$(this).css('font-size')+';line-height:'+$(this).css('line-height')+';font-family:'+$(this).css('font-family')+';white-space:'+$(this).css('white-space')+';word-wrap:'+$(this).css('word-wrap')+';letter-spacing:0.2px;" data="'+tData.replace(''+data+'','clone')+'"';
clone = '<div '+attr+'>'+$(this).val()+'</div>';
$('body').prepend(clone);
idNum++;
}else{
$('div[data="'+tData.replace(''+data+'','clone')+'"]').html($(this).val());
$(this).css('height',''+$('div[data="'+tData.replace(''+data+'','clone')+'"]').css('height')+'');
}
});