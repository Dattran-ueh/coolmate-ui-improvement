// Trần Gia Đạt – 31241024727
$(function(){$('[data-effect]').on('click',function(){const effect=$(this).attr('data-effect');$('figure').stop(true,true)[effect](600);});});