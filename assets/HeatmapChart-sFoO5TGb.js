import{g as ne,r as f,j as I}from"./index-BPEHoCFy.js";import{i as se,l as he,b as ge,r as me,a as ye,c as be,d as xe,k as wn,w as ve,t as Mn,s as rn,y as on,e as pe,z as Se,O as Dn,H as Pn,f as we,S as Me,j as Ce,I as ke,g as Yn,h as Ee,m as Le,n as Ie,o as Re,T as ze}from"./isDate-JiBEZXn_.js";function ee(){var n=0,e=1,r=1,t=[.5],i=[0,1],o;function u(d){return d!=null&&d<=d?i[ge(t,d,0,r)]:o}function a(){var d=-1;for(t=new Array(r);++d<r;)t[d]=((d+1)*e-(d-r)*n)/(r+1);return u}return u.domain=function(d){return arguments.length?([n,e]=d,n=+n,e=+e,a()):[n,e]},u.range=function(d){return arguments.length?(r=(i=Array.from(d)).length-1,a()):i.slice()},u.invertExtent=function(d){var l=i.indexOf(d);return l<0?[NaN,NaN]:l<1?[n,t[0]]:l>=r?[t[r-1],e]:[t[l-1],t[l]]},u.unknown=function(d){return arguments.length&&(o=d),u},u.thresholds=function(){return t.slice()},u.copy=function(){return ee().domain([n,e]).range(i).unknown(o)},se.apply(he(u),arguments)}var je=me();const Oe=ne(je);var mn,An;function Te(){if(An)return mn;An=1;var n=Math.ceil,e=Math.max;function r(t,i,o,u){for(var a=-1,d=e(n((i-t)/(o||1)),0),l=Array(d);d--;)l[u?d:++a]=t,t+=o;return l}return mn=r,mn}var yn,$n;function We(){if($n)return yn;$n=1;var n=/\s/;function e(r){for(var t=r.length;t--&&n.test(r.charAt(t)););return t}return yn=e,yn}var bn,Xn;function Fe(){if(Xn)return bn;Xn=1;var n=We(),e=/^\s+/;function r(t){return t&&t.slice(0,n(t)+1).replace(e,"")}return bn=r,bn}var xn,Zn;function qe(){if(Zn)return xn;Zn=1;var n=Fe(),e=ye(),r=be(),t=NaN,i=/^[-+]0x[0-9a-f]+$/i,o=/^0b[01]+$/i,u=/^0o[0-7]+$/i,a=parseInt;function d(l){if(typeof l=="number")return l;if(r(l))return t;if(e(l)){var j=typeof l.valueOf=="function"?l.valueOf():l;l=e(j)?j+"":j}if(typeof l!="string")return l===0?l:+l;l=n(l);var R=o.test(l);return R||u.test(l)?a(l.slice(2),R?2:8):i.test(l)?t:+l}return xn=d,xn}var vn,Gn;function Ve(){if(Gn)return vn;Gn=1;var n=qe(),e=1/0,r=17976931348623157e292;function t(i){if(!i)return i===0?i:0;if(i=n(i),i===e||i===-1/0){var o=i<0?-1:1;return o*r}return i===i?i:0}return vn=t,vn}var pn,Qn;function He(){if(Qn)return pn;Qn=1;var n=Te(),e=xe(),r=Ve();function t(i){return function(o,u,a){return a&&typeof a!="number"&&e(o,u,a)&&(u=a=void 0),o=r(o),u===void 0?(u=o,o=0):u=r(u),a=a===void 0?o<u?1:-1:r(a),n(o,u,a,i)}}return pn=t,pn}var Sn,Jn;function Ne(){if(Jn)return Sn;Jn=1;var n=He(),e=n();return Sn=e,Sn}var _e=Ne();const Be=ne(_e);function g(){return g=Object.assign?Object.assign.bind():function(n){for(var e=1;e<arguments.length;e++){var r=arguments[e];for(var t in r)Object.prototype.hasOwnProperty.call(r,t)&&(n[t]=r[t])}return n},g.apply(this,arguments)}function te(n,e){if(n==null)return{};var r,t,i={},o=Object.keys(n);for(t=0;t<o.length;t++)r=o[t],e.indexOf(r)>=0||(i[r]=n[r]);return i}var Kn;f.memo(function(n){var e=n.years,r=n.legend,t=n.theme;return I.jsx(I.Fragment,{children:e.map(function(i){return I.jsx("text",{transform:"translate("+i.x+","+i.y+") rotate("+i.rotation+")",textAnchor:"middle",style:t.labels.text,children:r(i.year)},i.year)})})});f.memo(function(n){var e=n.path,r=n.borderWidth,t=n.borderColor;return I.jsx("path",{d:e,style:{fill:"none",strokeWidth:r,stroke:t,pointerEvents:"none"}})});f.memo(function(n){var e=n.months,r=n.legend,t=n.theme;return I.jsx(I.Fragment,{children:e.map(function(i){return I.jsx("text",{transform:"translate("+i.x+","+i.y+") rotate("+i.rotation+")",textAnchor:"middle",style:t.labels.text,children:r(i.year,i.month,i.date)},i.date.toString()+".legend")})})});f.memo(function(n){var e=n.data,r=n.x,t=n.y,i=n.size,o=n.color,u=n.borderWidth,a=n.borderColor,d=n.isInteractive,l=n.tooltip,j=n.onMouseEnter,R=n.onMouseMove,E=n.onMouseLeave,m=n.onClick,p=n.formatValue,w=wn(),s=w.showTooltipFromEvent,y=w.hideTooltip,T=f.useCallback(function(L){if("value"in e){var H=g({},e,{value:p(e.value),data:g({},e.data)});s(f.createElement(l,g({},H)),L),j==null||j(e,L)}},[s,l,e,j,p]),W=f.useCallback(function(L){if("value"in e){var H=g({},e,{value:p(e.value),data:g({},e.data)});s(f.createElement(l,g({},H)),L),R&&R(e,L)}},[s,l,e,R,p]),S=f.useCallback(function(L){"value"in e&&(y(),E==null||E(e,L))},[y,e,E]),F=f.useCallback(function(L){return m==null?void 0:m(e,L)},[e,m]);return I.jsx("rect",{x:r,y:t,width:i,height:i,style:{fill:o,strokeWidth:u,stroke:a},onMouseEnter:d?T:void 0,onMouseMove:d?W:void 0,onMouseLeave:d?S:void 0,onClick:d?F:void 0})});var De=f.memo(function(n){var e=n.value,r=n.day,t=n.color;return e===void 0||isNaN(Number(e))?null:I.jsx(ve,{id:r,value:e,color:t,enableChip:!0})}),Pe=Mn("%b"),re={colors:["#61cdbb","#97e3d5","#e8c1a0","#f47560"],align:"center",direction:"horizontal",emptyColor:"#fff",minValue:0,maxValue:"auto",yearSpacing:30,yearLegend:function(n){return n},yearLegendPosition:"before",yearLegendOffset:10,monthBorderWidth:2,monthBorderColor:"#000",monthSpacing:0,monthLegend:function(n,e,r){return Pe(r)},monthLegendPosition:"before",monthLegendOffset:10,daySpacing:0,dayBorderWidth:1,dayBorderColor:"#000",isInteractive:!0,legends:[],tooltip:De},Ye=g({},re,{role:"img"}),v=g({},re,{pixelRatio:typeof window<"u"&&(Kn=window.devicePixelRatio)!=null?Kn:1});g({},Ye,{dayBorderColor:"#fff",dayRadius:0,square:!0,weekdayLegendOffset:75,firstWeekday:"sunday"});var Ae=function(n,e,r){var t=n.map(function(i){return i.value});return[e==="auto"?Math.min.apply(Math,t):e,r==="auto"?Math.max.apply(Math,t):r]},$e=Oe(function(n){var e,r=n.date,t=n.cellSize,i=n.yearIndex,o=n.yearSpacing,u=n.monthSpacing,a=n.daySpacing,d=n.direction,l=n.originX,j=n.originY,R=new Date(r.getFullYear(),r.getMonth()+1,0),E=rn.count(on(r),r),m=rn.count(on(R),R),p=r.getDay(),w=R.getDay(),s=l,y=j,T=i*(7*(t+a)+o),W=r.getMonth()*u;d==="horizontal"?(y+=T,s+=W):(y+=W,s+=T);var S={x:s,y,width:0,height:0};return d==="horizontal"?(e=["M"+(s+(E+1)*(t+a))+","+(y+p*(t+a)),"H"+(s+E*(t+a))+"V"+(y+7*(t+a)),"H"+(s+m*(t+a))+"V"+(y+(w+1)*(t+a)),"H"+(s+(m+1)*(t+a))+"V"+y,"H"+(s+(E+1)*(t+a))+"Z"].join(""),S.x=s+E*(t+a),S.width=s+(m+1)*(t+a)-S.x,S.height=7*(t+a)):(e=["M"+(s+p*(t+a))+","+(y+(E+1)*(t+a)),"H"+s+"V"+(y+(m+1)*(t+a)),"H"+(s+(w+1)*(t+a))+"V"+(y+m*(t+a)),"H"+(s+7*(t+a))+"V"+(y+E*(t+a)),"H"+(s+p*(t+a))+"Z"].join(""),S.y=y+E*(t+a),S.width=7*(t+a),S.height=y+(m+1)*(t+a)-S.y),{path:e,bbox:S}},function(n){var e=n.date,r=n.cellSize,t=n.yearIndex,i=n.yearSpacing,o=n.monthSpacing,u=n.daySpacing,a=n.direction,d=n.originX,l=n.originY;return e.toString()+"."+r+"."+t+"."+i+"."+o+"."+u+"."+a+"."+d+"."+l}),Xe=Mn("%Y-%m-%d"),Ze=function(n){var e,r=n.width,t=n.height,i=n.from,o=n.to,u=n.direction,a=n.yearSpacing,d=n.monthSpacing,l=n.daySpacing,j=n.align,R=Yn(i)?i:new Date(i),E=Yn(o)?o:new Date(o),m=Be(R.getFullYear(),E.getFullYear()+1),p=Math.max.apply(Math,m.map(function(h){return Ee(new Date(h,0,1),new Date(h+1,0,1)).length}))+1,w=function(h){var b,z,M=h.width,C=h.height,O=h.direction,k=h.yearRange,N=h.yearSpacing,P=h.monthSpacing,$=h.daySpacing,Y=h.maxWeeks;return O==="horizontal"?(b=(M-12*P-$*Y)/Y,z=(C-(k.length-1)*N-k.length*(8*$))/(7*k.length)):(b=(M-(k.length-1)*N-k.length*(8*$))/(7*k.length),z=(C-12*P-$*Y)/Y),Math.min(b,z)}({width:r,height:t,direction:u,yearRange:m,yearSpacing:a,monthSpacing:d,daySpacing:l,maxWeeks:p}),s=w*p+l*p+12*d,y=7*(w+l)*m.length+a*(m.length-1),T=u==="horizontal"?s:y,W=u==="horizontal"?y:s,S=ze({x:0,y:0,width:T,height:W},{x:0,y:0,width:r,height:t},j),F=S[0],L=S[1];e=u==="horizontal"?function(h,b,z,M){return function(C,O,k,N){return{x:C+rn.count(on(k),k)*(h+M)+M/2+k.getMonth()*z,y:O+k.getDay()*(h+M)+M/2+N*(b+7*(h+M))}}}(w,a,d,l):function(h,b,z,M){return function(C,O,k,N){var P=rn.count(on(k),k);return{x:C+k.getDay()*(h+M)+M/2+N*(b+7*(h+M)),y:O+P*(h+M)+M/2+k.getMonth()*z}}}(w,a,d,l);var H=[],_=[],B=[];return m.forEach(function(h,b){var z=new Date(h,0,1),M=new Date(h+1,0,1);B=B.concat(Le(z,M).map(function(O){return g({date:O,day:Xe(O),size:w},e(F,L,O,b))}));var C=Ie(z,M).map(function(O){return g({date:O,year:O.getFullYear(),month:O.getMonth()},$e({originX:F,originY:L,date:O,direction:u,yearIndex:b,yearSpacing:a,monthSpacing:d,daySpacing:l,cellSize:w}))});_=_.concat(C),H.push({year:h,bbox:{x:C[0].bbox.x,y:C[0].bbox.y,width:C[11].bbox.x-C[0].bbox.x+C[11].bbox.width,height:C[11].bbox.y-C[0].bbox.y+C[11].bbox.height}})}),{years:H,months:_,days:B,cellSize:w,calendarWidth:T,calendarHeight:W,originX:F,originY:L}},Ge=function(n){var e=n.days,r=n.data,t=n.colorScale,i=n.emptyColor;return e.map(function(o){var u=r.find(function(a){return a.day===o.day});return g({},o,u?{color:t(u.value),data:u,value:u.value}:{color:i})})},Qe=function(n){var e=n.years,r=n.direction,t=n.position,i=n.offset;return e.map(function(o){var u=0,a=0,d=0;return r==="horizontal"&&t==="before"?(u=o.bbox.x-i,a=o.bbox.y+o.bbox.height/2,d=-90):r==="horizontal"&&t==="after"?(u=o.bbox.x+o.bbox.width+i,a=o.bbox.y+o.bbox.height/2,d=-90):r==="vertical"&&t==="before"?(u=o.bbox.x+o.bbox.width/2,a=o.bbox.y-i):(u=o.bbox.x+o.bbox.width/2,a=o.bbox.y+o.bbox.height+i),g({},o,{x:u,y:a,rotation:d})})},Je=function(n){var e=n.months,r=n.direction,t=n.position,i=n.offset;return e.map(function(o){var u=0,a=0,d=0;return r==="horizontal"&&t==="before"?(u=o.bbox.x+o.bbox.width/2,a=o.bbox.y-i):r==="horizontal"&&t==="after"?(u=o.bbox.x+o.bbox.width/2,a=o.bbox.y+o.bbox.height+i):r==="vertical"&&t==="before"?(u=o.bbox.x-i,a=o.bbox.y+o.bbox.height/2,d=-90):(u=o.bbox.x+o.bbox.width+i,a=o.bbox.y+o.bbox.height/2,d=-90),g({},o,{x:u,y:a,rotation:d})})},Ke=function(n){var e=n.width,r=n.height,t=n.from,i=n.to,o=n.direction,u=n.yearSpacing,a=n.monthSpacing,d=n.daySpacing,l=n.align;return f.useMemo(function(){return Ze({width:e,height:r,from:t,to:i,direction:o,yearSpacing:u,monthSpacing:a,daySpacing:d,align:l})},[e,r,t,i,o,u,a,d,l])},Ue=function(n){var e=n.data,r=n.minValue,t=n.maxValue,i=n.colors,o=n.colorScale;return f.useMemo(function(){if(o)return o;var u=Ae(e,r,t);return ee().domain(u).range(i)},[e,r,t,i,o])},nt=function(n){var e=n.years,r=n.direction,t=n.yearLegendPosition,i=n.yearLegendOffset;return f.useMemo(function(){return Qe({years:e,direction:r,position:t,offset:i})},[e,r,t,i])},et=function(n){var e=n.months,r=n.direction,t=n.monthLegendPosition,i=n.monthLegendOffset;return f.useMemo(function(){return Je({months:e,direction:r,position:t,offset:i})},[e,r,t,i])},tt=function(n){var e=n.days,r=n.data,t=n.colorScale,i=n.emptyColor;return f.useMemo(function(){return Ge({days:e,data:r,colorScale:t,emptyColor:i})},[e,r,t,i])};Mn("%Y-%m-%d");f.memo(function(n){var e=n.data,r=n.x,t=n.ry,i=t===void 0?5:t,o=n.rx,u=o===void 0?5:o,a=n.y,d=n.width,l=n.height,j=n.color,R=n.borderWidth,E=n.borderColor,m=n.isInteractive,p=n.tooltip,w=n.onMouseEnter,s=n.onMouseMove,y=n.onMouseLeave,T=n.onClick,W=n.formatValue,S=wn(),F=S.showTooltipFromEvent,L=S.hideTooltip,H=f.useCallback(function(b){if("value"in e){var z=g({},e,{value:W(e.value)});F(f.createElement(p,g({},z)),b),w==null||w(e,b)}},[F,p,e,w,W]),_=f.useCallback(function(b){if("value"in e){var z=g({},e,{value:W(e.value)});F(f.createElement(p,g({},z)),b),s==null||s(e,b)}},[F,p,e,s,W]),B=f.useCallback(function(b){"value"in e&&(L(),y==null||y(e,b))},[L,e,y]),h=f.useCallback(function(b){return T==null?void 0:T(e,b)},[e,T]);return I.jsx("rect",{x:r,y:a,rx:u,ry:i,width:d,height:l,style:{fill:j,strokeWidth:R,stroke:E},onMouseEnter:m?H:void 0,onMouseMove:m?_:void 0,onMouseLeave:m?B:void 0,onClick:m?h:void 0})});var rt=["months","years"],it=["isInteractive","renderWrapper","theme"],Un=function(n,e,r,t,i,o){var u=Me(e,n),a=u[0],d=u[1];return r.find(function(l){return"value"in l&&Ce(l.x+o.left-i/2,l.y+o.top-i/2,t+i,t+i,a,d)})},ot=f.memo(function(n){var e=n.margin,r=n.width,t=n.height,i=n.pixelRatio,o=i===void 0?v.pixelRatio:i,u=n.align,a=u===void 0?v.align:u,d=n.colors,l=d===void 0?v.colors:d,j=n.colorScale,R=n.data,E=n.direction,m=E===void 0?v.direction:E,p=n.emptyColor,w=p===void 0?v.emptyColor:p,s=n.from,y=n.to,T=n.minValue,W=T===void 0?v.minValue:T,S=n.maxValue,F=S===void 0?v.maxValue:S,L=n.valueFormat,H=n.legendFormat,_=n.yearLegend,B=_===void 0?v.yearLegend:_,h=n.yearLegendOffset,b=h===void 0?v.yearLegendOffset:h,z=n.yearLegendPosition,M=z===void 0?v.yearLegendPosition:z,C=n.yearSpacing,O=C===void 0?v.yearSpacing:C,k=n.monthLegend,N=k===void 0?v.monthLegend:k,P=n.monthLegendOffset,$=P===void 0?v.monthLegendOffset:P,Y=n.monthLegendPosition,ie=Y===void 0?v.monthLegendPosition:Y,Cn=n.monthSpacing,oe=Cn===void 0?v.monthSpacing:Cn,kn=n.dayBorderColor,En=kn===void 0?v.dayBorderColor:kn,Ln=n.dayBorderWidth,X=Ln===void 0?v.dayBorderWidth:Ln,In=n.daySpacing,an=In===void 0?v.daySpacing:In,Rn=n.isInteractive,U=Rn===void 0?v.isInteractive:Rn,zn=n.tooltip,jn=zn===void 0?v.tooltip:zn,un=n.onClick,dn=n.onMouseEnter,G=n.onMouseLeave,ln=n.onMouseMove,On=n.legends,Tn=On===void 0?v.legends:On,q=f.useRef(null),Q=pe(r,t,e),cn=Q.innerWidth,fn=Q.innerHeight,J=Q.outerWidth,K=Q.outerHeight,A=Q.margin,sn=Ke({width:cn,height:fn,from:s,to:y,direction:m,yearSpacing:O,monthSpacing:oe,daySpacing:an,align:a}),ae=sn.months,ue=sn.years,de=te(sn,rt),nn=Ue({data:R,minValue:W,maxValue:F,colors:l,colorScale:j}),Wn=et({months:ae,direction:m,monthLegendPosition:ie,monthLegendOffset:$}),Fn=nt({years:ue,direction:m,yearLegendPosition:M,yearLegendOffset:b}),D=tt({days:de.days,data:R,colorScale:nn,emptyColor:w}),qn=f.useState(null),hn=qn[0],en=qn[1],Z=Se(),Vn=Dn(L),Hn=Dn(H),Nn=wn(),_n=Nn.showTooltipFromEvent,tn=Nn.hideTooltip;f.useEffect(function(){var V;if(q.current){q.current.width=J*o,q.current.height=K*o;var c=q.current.getContext("2d");c&&(c.scale(o,o),c.fillStyle=Z.background,c.fillRect(0,0,J,K),c.translate(A.left,A.top),D.forEach(function(x){c.fillStyle=x.color,X>0&&(c.strokeStyle=En,c.lineWidth=X),c.beginPath(),c.rect(x.x,x.y,x.size,x.size),c.fill(),X>0&&c.stroke()}),c.textAlign="center",c.textBaseline="middle",c.fillStyle=(V=Z.labels.text.fill)!=null?V:"",c.font=Z.labels.text.fontSize+"px "+Z.labels.text.fontFamily,Wn.forEach(function(x){c.save(),c.translate(x.x,x.y),c.rotate(Pn(x.rotation)),c.fillText(String(N(x.year,x.month,x.date)),0,0),c.restore()}),Fn.forEach(function(x){c.save(),c.translate(x.x,x.y),c.rotate(Pn(x.rotation)),c.fillText(String(B(x.year)),0,0),c.restore()}),Tn.forEach(function(x){var fe=nn.ticks(x.itemCount).map(function(gn){return{id:gn,label:Hn(gn),color:nn(gn)}});we(c,g({},x,{data:fe,containerWidth:cn,containerHeight:fn,theme:Z}))}))}},[q,fn,cn,J,K,o,A,D,En,X,j,B,Fn,N,Wn,Tn,Z,Hn,nn]);var Bn=f.useCallback(function(V){if(q.current){var c=Un(V,q.current,D,D[0].size,X,A);if(c){if(en(c),!("value"in c))return;var x=g({},c,{value:Vn(c.value),data:g({},c.data)});_n(f.createElement(jn,g({},x)),V),!hn&&(dn==null||dn(c,V)),ln==null||ln(c,V),hn&&(G==null||G(c,V))}else tn(),c&&(G==null||G(c,V))}},[q,hn,A,D,en,Vn,X,_n,tn,dn,ln,G,jn]),le=f.useCallback(function(){en(null),tn()},[en,tn]),ce=f.useCallback(function(V){if(un&&q.current){var c=Un(V,q.current,D,D[0].size,an,A);c&&un(c,V)}},[q,an,A,D,un]);return I.jsx("canvas",{ref:q,width:J*o,height:K*o,style:{width:J,height:K},onMouseEnter:U?Bn:void 0,onMouseMove:U?Bn:void 0,onMouseLeave:U?le:void 0,onClick:U?ce:void 0})}),at=function(n){var e=n.isInteractive,r=e===void 0?v.isInteractive:e,t=n.renderWrapper,i=n.theme,o=te(n,it);return I.jsx(Re,{isInteractive:r,renderWrapper:t,theme:i,children:I.jsx(ot,g({isInteractive:r},o))})},ut=function(n){return I.jsx(ke,{children:function(e){var r=e.width,t=e.height;return I.jsx(at,g({width:r,height:t},n))}})};const dt={},ft=({data:n})=>{const e=n.map(r=>({day:new Date(r.date).toISOString().split("T")[0],value:r.points}));return I.jsx(I.Fragment,{children:I.jsx(ut,{data:e,from:e[0].day,to:e[e.length-1].day,emptyColor:"#eeeeee",colors:["#A8E6CF","#77DD77"],margin:{top:40,right:40,bottom:40,left:40},yearSpacing:40,monthBorderColor:"#ffffff",dayBorderWidth:2,dayBorderColor:"#ffffff",theme:dt,legends:[{anchor:"bottom-right",direction:"row",translateY:36,itemCount:4,itemWidth:42,itemHeight:36,itemsSpacing:14,itemDirection:"right-to-left"}]})})};export{ft as default};
