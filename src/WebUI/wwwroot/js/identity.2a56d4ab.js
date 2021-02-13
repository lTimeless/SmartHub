(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["identity"],{1326:function(e,t,r){"use strict";r("99af");var n=r("7a23"),o=Object(n["withScopeId"])("data-v-49072921"),c=o((function(e,t,r,o,c,a){return e.ready?(Object(n["openBlock"])(),Object(n["createBlock"])("div",{key:0,class:"loader ease-linear rounded-full border-8 border-t-8 border-gray-200 ".concat(e.height," ").concat(e.width)},null,2)):Object(n["createCommentVNode"])("",!0)})),a=r("7bbe"),s=Object(n["defineComponent"])({name:"AppSpinner",props:{height:{type:String,required:!0},width:{type:String,required:!0}},setup:function(){var e=Object(a["o"])(300,!0),t=e.ready;return{ready:t}}});r("2749");s.render=c,s.__scopeId="data-v-49072921";t["a"]=s},2749:function(e,t,r){"use strict";r("c3cc")},"2c75":function(e,t,r){"use strict";r.r(t);r("b0c0");var n=r("7a23"),o=r("c874"),c=r.n(o),a={class:"flex items-center min-h-screen p-6 background"},s={key:0,class:"flex items-center justify-center w-full h-108"},i={key:1,class:"flex items-center justify-center w-full h-108"},l=Object(n["createVNode"])("div",{class:"hidden md:block h-108 md:h-auto md:w-1/2"},[Object(n["createVNode"])("img",{"aria-hidden":"true",class:"object-fill w-full h-full dark:hidden",src:c.a,alt:"Office"})],-1),d={class:"flex items-center justify-center h-108 p-6 sm:p-12 md:w-1/2"},u={class:"w-full"},b={class:"mb-4 text-left text-2xl font-semibold text-primarySienna"},p={class:"text-left block text-sm"},f=Object(n["createVNode"])("span",{class:"text-gray-600 dark:text-gray-400"},"Username",-1),g={class:"text-left block mt-4 text-sm"},m=Object(n["createVNode"])("span",{class:"text-gray-600 dark:text-gray-400"},"Password",-1),j={class:"flex content-center justify-center"},x=Object(n["createVNode"])("span",{class:"pl-2"},"Log in",-1),O={key:0,class:"flex mt-4 text-sm"},w={class:"text-red-400"},h=Object(n["createVNode"])("hr",{class:"my-8"},null,-1),v={class:"mt-4 text-left"},y=Object(n["createTextVNode"])(" Forgot your password? "),k={class:"mt-1 text-left"},N=Object(n["createTextVNode"])(" Create account ");function V(e,t,r,o,c,V){var C=Object(n["resolveComponent"])("Loader"),S=Object(n["resolveComponent"])("router-link"),R=Object(n["resolveComponent"])("AppCard");return Object(n["openBlock"])(),Object(n["createBlock"])("div",a,[Object(n["createVNode"])(R,{class:"bg-white"},{default:Object(n["withCtx"])((function(){return[e.loading?(Object(n["openBlock"])(),Object(n["createBlock"])("div",s,[Object(n["createVNode"])(C,{height:"h-48",width:"w-48"})])):e.error?(Object(n["openBlock"])(),Object(n["createBlock"])("div",i,[Object(n["createVNode"])("p",null,"Error: "+Object(n["toDisplayString"])(e.error.name)+" "+Object(n["toDisplayString"])(e.error.message),1)])):(Object(n["openBlock"])(),Object(n["createBlock"])(n["Fragment"],{key:2},[l,Object(n["createVNode"])("div",d,[Object(n["createVNode"])("div",u,[Object(n["createVNode"])("h2",b,Object(n["toDisplayString"])(e.title),1),Object(n["createVNode"])("label",p,[f,Object(n["withDirectives"])(Object(n["createVNode"])("input",{required:"",type:"text","onUpdate:modelValue":t[1]||(t[1]=function(t){return e.userName=t}),class:"mt-1 focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded",placeholder:"Jane Doe"},null,512),[[n["vModelText"],e.userName]])]),Object(n["createVNode"])("label",g,[m,Object(n["withDirectives"])(Object(n["createVNode"])("input",{required:"","onUpdate:modelValue":t[2]||(t[2]=function(t){return e.password=t}),class:"mt-1 focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded",placeholder:"***************",type:"password",onKeyup:t[3]||(t[3]=Object(n["withKeys"])((function(){return e.onLoginClick&&e.onLoginClick.apply(e,arguments)}),["enter"]))},null,544),[[n["vModelText"],e.password]])]),Object(n["createVNode"])("button",{class:["block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white bg-primarySienna rounded",e.signInDisabled?"opacity-50 focus:outline-none cursor-not-allowed":"hover:bg-primarySiennaHover"],onClick:t[4]||(t[4]=function(){return e.onLoginClick&&e.onLoginClick.apply(e,arguments)}),disabled:e.signInDisabled},[Object(n["createVNode"])("span",j,[e.loadLogin?(Object(n["openBlock"])(),Object(n["createBlock"])(C,{key:0,height:"h-2",width:"w-2"})):Object(n["createCommentVNode"])("",!0),x])],10,["disabled"]),e.errLogin?(Object(n["openBlock"])(),Object(n["createBlock"])("div",O,[Object(n["createVNode"])("span",w,Object(n["toDisplayString"])(e.errLogin.name)+": "+Object(n["toDisplayString"])(e.errLogin.message),1)])):Object(n["createCommentVNode"])("",!0),h,Object(n["createVNode"])("button",{disabled:"",class:["flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray","opacity-50 focus:outline-none cursor-not-allowed"]}," Additional login options.... "),Object(n["createVNode"])("p",v,[Object(n["createVNode"])(S,{class:"text-sm font-medium text-primarySienna hover:underline",to:"/forgotpassword"},{default:Object(n["withCtx"])((function(){return[y]})),_:1})]),Object(n["createVNode"])("p",k,[Object(n["createVNode"])(S,{class:"text-sm font-medium text-primarySienna hover:underline",to:"/registration"},{default:Object(n["withCtx"])((function(){return[N]})),_:1})])])])],64))]})),_:1})])}r("d3b7"),r("96cf");var C=r("1da1"),S=r("8785"),R=r("6c02"),P=r("091e"),B=r("a007"),L=r("1326"),D=r("dc2f"),I=r("9530"),E=r.n(I);function T(){var e=Object(S["a"])(["\n  mutation Login($input: LoginInput!) {\n    login(input: $input) {\n      token\n      user {\n        id\n        userName\n        isFirstLogin\n      }\n      errors {\n        message\n        code\n      }\n      message\n    }\n  }\n"]);return T=function(){return e},e}var A=E()(T()),_=r("b2ee");function U(){var e=Object(S["a"])(["\n  query HomeAndUsersExist {\n    applicationIsActive\n    usersExist\n  }\n"]);return U=function(){return e},e}var M=E()(U()),q=Object(n["defineComponent"])({name:"Login",components:{Loader:L["a"],AppCard:D["a"]},props:{},setup:function(){var e=Object(R["d"])(),t=Object(_["a"])(),r=t.token,o="Login",c=Object(n["ref"])(""),a=Object(n["ref"])(""),s=Object(n["reactive"])({userName:"",password:""}),i=Object(P["b"])(A),l=i.executeMutation,d=i.fetching,u=i.error,b=Object(P["c"])({query:M}),p=b.data,f=b.fetching,g=b.error,m=Object(n["computed"])((function(){return p.value}));Object(n["watch"])(m,(function(t){return t.applicationIsActive?t.usersExist?void 0:(e.push(B["d"].Registration),Promise.resolve()):(e.push(B["d"].Init),Promise.resolve())}));var j=function(){var t=Object(C["a"])(regeneratorRuntime.mark((function t(){return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return s.userName=a.value,s.password=c.value,t.next=4,l({input:s}).then((function(t){t.data&&t.data.login.token&&(r.value=t.data.login.token,e.push(B["d"].Home))}));case 4:case"end":return t.stop()}}),t)})));return function(){return t.apply(this,arguments)}}(),x=Object(n["computed"])((function(){return 0===a.value.length||c.value.length<4}));return{loadLogin:d,errLogin:u,error:g,loading:f,title:o,onLoginClick:j,password:c,userName:a,signInDisabled:x}}});q.render=V;t["default"]=q},"4d63":function(e,t,r){var n=r("83ab"),o=r("da84"),c=r("94ca"),a=r("7156"),s=r("9bf2").f,i=r("241c").f,l=r("44e7"),d=r("ad6d"),u=r("9f7f"),b=r("6eeb"),p=r("d039"),f=r("69f3").set,g=r("2626"),m=r("b622"),j=m("match"),x=o.RegExp,O=x.prototype,w=/a/g,h=/a/g,v=new x(w)!==w,y=u.UNSUPPORTED_Y,k=n&&c("RegExp",!v||y||p((function(){return h[j]=!1,x(w)!=w||x(h)==h||"/a/i"!=x(w,"i")})));if(k){var N=function(e,t){var r,n=this instanceof N,o=l(e),c=void 0===t;if(!n&&o&&e.constructor===N&&c)return e;v?o&&!c&&(e=e.source):e instanceof N&&(c&&(t=d.call(e)),e=e.source),y&&(r=!!t&&t.indexOf("y")>-1,r&&(t=t.replace(/y/g,"")));var s=a(v?new x(e,t):x(e,t),n?this:O,N);return y&&r&&f(s,{sticky:r}),s},V=function(e){e in N||s(N,e,{configurable:!0,get:function(){return x[e]},set:function(t){x[e]=t}})},C=i(x),S=0;while(C.length>S)V(C[S++]);O.constructor=N,N.prototype=O,b(o,"RegExp",N)}g("RegExp")},5440:function(e,t,r){"use strict";r("c5dc")},9263:function(e,t,r){"use strict";var n=r("ad6d"),o=r("9f7f"),c=RegExp.prototype.exec,a=String.prototype.replace,s=c,i=function(){var e=/a/,t=/b*/g;return c.call(e,"a"),c.call(t,"a"),0!==e.lastIndex||0!==t.lastIndex}(),l=o.UNSUPPORTED_Y||o.BROKEN_CARET,d=void 0!==/()??/.exec("")[1],u=i||d||l;u&&(s=function(e){var t,r,o,s,u=this,b=l&&u.sticky,p=n.call(u),f=u.source,g=0,m=e;return b&&(p=p.replace("y",""),-1===p.indexOf("g")&&(p+="g"),m=String(e).slice(u.lastIndex),u.lastIndex>0&&(!u.multiline||u.multiline&&"\n"!==e[u.lastIndex-1])&&(f="(?: "+f+")",m=" "+m,g++),r=new RegExp("^(?:"+f+")",p)),d&&(r=new RegExp("^"+f+"$(?!\\s)",p)),i&&(t=u.lastIndex),o=c.call(b?r:u,m),b?o?(o.input=o.input.slice(g),o[0]=o[0].slice(g),o.index=u.lastIndex,u.lastIndex+=o[0].length):u.lastIndex=0:i&&o&&(u.lastIndex=u.global?o.index+o[0].length:t),d&&o&&o.length>1&&a.call(o[0],r,(function(){for(s=1;s<arguments.length-2;s++)void 0===arguments[s]&&(o[s]=void 0)})),o}),e.exports=s},"9f7f":function(e,t,r){"use strict";var n=r("d039");function o(e,t){return RegExp(e,t)}t.UNSUPPORTED_Y=n((function(){var e=o("a","y");return e.lastIndex=2,null!=e.exec("abcd")})),t.BROKEN_CARET=n((function(){var e=o("^r","gy");return e.lastIndex=2,null!=e.exec("str")}))},ac1f:function(e,t,r){"use strict";var n=r("23e7"),o=r("9263");n({target:"RegExp",proto:!0,forced:/./.exec!==o},{exec:o})},c3cc:function(e,t,r){},c5dc:function(e,t,r){},c874:function(e,t,r){e.exports=r.p+"img/undraw_smart_home_28oy.da6db3b3.svg"},dc2f:function(e,t,r){"use strict";var n=r("7a23"),o={class:"relative flex-1 h-full max-w-4xl mx-auto overflow-hidden rounded p-3"},c={class:"flex flex-col overflow-y-auto md:flex-row"};function a(e,t,r,a,s,i){return Object(n["openBlock"])(),Object(n["createBlock"])("div",o,[Object(n["createVNode"])("div",c,[Object(n["renderSlot"])(e.$slots,"default")])])}var s=Object(n["defineComponent"])({name:"AppCard"});s.render=a;t["a"]=s},e592:function(e,t,r){"use strict";r.r(t);var n=r("7a23"),o=r("c874"),c=r.n(o),a=Object(n["withScopeId"])("data-v-adadc650");Object(n["pushScopeId"])("data-v-adadc650");var s={class:"flex items-center min-h-screen p-6 background"},i=Object(n["createVNode"])("div",{class:"hidden md:block h-108 md:h-auto md:w-1/2"},[Object(n["createVNode"])("img",{"aria-hidden":"true",class:"object-fill w-full h-full dark:hidden",src:c.a,alt:"Office"})],-1),l={class:"flex items-center justify-center h-108 p-6 sm:p-12 md:w-1/2"},d={class:"w-full"},u={class:"mb-4 text-left text-2xl font-semibold text-primarySienna"},b={class:"text-left block text-sm"},p=Object(n["createVNode"])("span",{class:"text-gray-600 dark:text-gray-400"},"Username",-1),f={class:"relative"},g={class:"text-left block mt-4 text-sm"},m=Object(n["createVNode"])("span",{class:"text-gray-600 dark:text-gray-400"}," Password ",-1),j=Object(n["createVNode"])("path",{d:"M12 19c.946 0 1.81-.103 2.598-.281l-1.757-1.757C12.568 16.983 12.291 17 12 17c-5.351\n                                    0-7.424-3.846-7.926-5 .204-.47.674-1.381 1.508-2.297L4.184 8.305c-1.538 1.667-2.121 3.346-2.132\n                                    3.379-.069.205-.069.428 0 .633C2.073 12.383 4.367 19 12 19zM12 5c-1.837 0-3.346.396-4.604.981L3.707\n                                    2.293 2.293 3.707l18 18 1.414-1.414-3.319-3.319c2.614-1.951 3.547-4.615 3.561-4.657.069-.205.069-.428\n                                    0-.633C21.927 11.617 19.633 5 12 5zM16.972 15.558l-2.28-2.28C14.882 12.888 15 12.459 15\n                                    12c0-1.641-1.359-3-3-3-.459 0-.888.118-1.277.309L8.915 7.501C9.796 7.193 10.814 7 12 7c5.351\n                                    0 7.424 3.846 7.926 5C19.624 12.692 18.76 14.342 16.972 15.558z"},null,-1),x=Object(n["createVNode"])("path",{d:"M12,9c-1.642,0-3,1.359-3,3c0,1.642,1.358,3,3,3c1.641,0,3-1.358,3-3C15,10.359,13.641,9,12,9z"},null,-1),O=Object(n["createVNode"])("path",{d:"M12,5c-7.633,0-9.927,6.617-9.948,6.684L1.946,12l0.105,0.316C2.073,12.383,4.367,19,12,\n                                    19s9.927-6.617,9.948-6.684 L22.054,12l-0.105-0.316C21.927,11.617,19.633,5,12,5z M12,17c-5.351,\n                                    0-7.424-3.846-7.926-5C4.578,10.842,6.652,7,12,7 c5.351,0,7.424,3.846,7.926,5C19.422,13.158,17.348,17,12,17z"},null,-1),w={class:"relative"},h={class:"text-left block mt-4 text-sm"},v=Object(n["createVNode"])("span",{class:"text-gray-600 dark:text-gray-400"}," Confirm password ",-1),y={class:"flex items-center mt-4 h-3"},k={class:"w-2/3 flex justify-between h-2"},N={class:"text-gray-500 font-medium text-sm ml-3 leading-none"},V={class:"flex mt-4 text-sm"},C=Object(n["createVNode"])("span",{class:"text-red-400"},"Password is not identical ",-1),S={class:"flex content-center justify-center"},R=Object(n["createVNode"])("span",{class:"pl-2"},"Create account",-1),P=Object(n["createVNode"])("hr",{class:"my-8"},null,-1),B={class:"mt-4 text-left"},L=Object(n["createTextVNode"])(" Already have an account? Login ");Object(n["popScopeId"])();var D=a((function(e,t,r,o,c,D){var I=Object(n["resolveComponent"])("Loader"),E=Object(n["resolveComponent"])("router-link"),T=Object(n["resolveComponent"])("AppCard");return Object(n["openBlock"])(),Object(n["createBlock"])("div",s,[Object(n["createVNode"])(T,{class:"bg-white border"},{default:a((function(){return[i,Object(n["createVNode"])("div",l,[Object(n["createVNode"])("div",d,[Object(n["createVNode"])("h2",u,Object(n["toDisplayString"])(e.title),1),Object(n["createVNode"])("label",b,[p,Object(n["withDirectives"])(Object(n["createVNode"])("input",{type:"text","onUpdate:modelValue":t[1]||(t[1]=function(t){return e.registrationRequest.userName=t}),class:"mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded",placeholder:"Jane Doe"},null,512),[[n["vModelText"],e.registrationRequest.userName]])]),Object(n["createVNode"])("div",f,[Object(n["createVNode"])("label",g,[m,Object(n["withDirectives"])(Object(n["createVNode"])("input",{type:e.togglePassword?"text":"password",onKeydown:t[2]||(t[2]=function(){return e.checkPasswordStrength&&e.checkPasswordStrength.apply(e,arguments)}),onBlur:t[3]||(t[3]=function(){return e.checkPasswordStrength&&e.checkPasswordStrength.apply(e,arguments)}),"onUpdate:modelValue":t[4]||(t[4]=function(t){return e.registrationRequest.password=t}),class:"mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded",placeholder:"***************"},null,40,["type"]),[[n["vModelDynamic"],e.registrationRequest.password]]),Object(n["createVNode"])("div",{class:"absolute right-0 bottom-0 flex flex-col justify-end top-0 px-3 py-2 cursor-pointer",onClick:t[5]||(t[5]=function(t){return e.togglePassword=!e.togglePassword})},[(Object(n["openBlock"])(),Object(n["createBlock"])("svg",{class:[{hidden:!e.togglePassword,block:e.togglePassword},"w-6 h-6 fill-current text-gray-500"],xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 24 24"},[j],2)),(Object(n["openBlock"])(),Object(n["createBlock"])("svg",{class:[{hidden:e.togglePassword,block:!e.togglePassword},"w-6 h-6 fill-current text-gray-500"],xmlns:"http://www.w3.org/2000/svg",viewBox:"0 0 24 24"},[x,O],2))])])]),Object(n["createVNode"])("div",w,[Object(n["createVNode"])("label",h,[v,Object(n["withDirectives"])(Object(n["createVNode"])("input",{type:e.togglePassword?"text":"password","onUpdate:modelValue":t[6]||(t[6]=function(t){return e.confirmPwd=t}),class:"mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded",placeholder:"***************"},null,8,["type"]),[[n["vModelDynamic"],e.confirmPwd]])])]),Object(n["createVNode"])("div",y,[Object(n["createVNode"])("div",k,[Object(n["createVNode"])("div",{class:[{"bg-red-400":"Too weak"===e.passwordStrengthText||"Could be stronger"===e.passwordStrengthText||"Strong password"===e.passwordStrengthText},"h-2 rounded-full mr-1 w-1/3 bg-gray-300"]},null,2),Object(n["createVNode"])("div",{class:[{"bg-yellow-400":"Could be stronger"===e.passwordStrengthText||"Strong password"===e.passwordStrengthText},"h-2 rounded-full mr-1 w-1/3 bg-gray-300"]},null,2),Object(n["createVNode"])("div",{class:[{"bg-green-400":"Strong password"===e.passwordStrengthText},"h-2 rounded-full w-1/3 bg-gray-300"]},null,2)]),Object(n["createVNode"])("div",N,Object(n["toDisplayString"])(e.passwordStrengthText),1)]),Object(n["withDirectives"])(Object(n["createVNode"])("div",V,[C],512),[[n["vShow"],e.registrationRequest.password!==e.confirmPwd]]),Object(n["createVNode"])("button",{class:["block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white bg-primarySienna rounded",e.registrationDisabled?"opacity-50 focus:outline-none cursor-not-allowed":"hover:bg-primarySiennaHover"],onClick:t[7]||(t[7]=function(){return e.onRegistrationClick&&e.onRegistrationClick.apply(e,arguments)}),disabled:e.registrationDisabled},[Object(n["createVNode"])("span",S,[e.loadCreate?(Object(n["openBlock"])(),Object(n["createBlock"])(I,{key:0,height:"h-2",width:"w-2"})):Object(n["createCommentVNode"])("",!0),R])],10,["disabled"]),P,Object(n["createVNode"])("button",{disabled:"",class:["flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-black transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400 active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray","opacity-50 focus:outline-none cursor-not-allowed"]}," Additional options.... "),Object(n["createVNode"])("p",B,[Object(n["createVNode"])(E,{class:"text-sm font-medium text-primarySienna hover:underline",to:"/login"},{default:a((function(){return[L]})),_:1})])])])]})),_:1})])})),I=(r("d3b7"),r("4d63"),r("ac1f"),r("25f0"),r("96cf"),r("1da1")),E=r("6c02"),T=r("1326"),A=r("a007"),_=r("dc2f"),U=r("091e"),M=r("8785"),q=r("9530"),z=r.n(q);function $(){var e=Object(M["a"])(["\n  mutation Registration($input: RegistrationInput!) {\n    registration(input: $input) {\n      token\n      user {\n        id\n        userName\n      }\n      errors {\n        message\n        code\n      }\n      message\n    }\n  }\n"]);return $=function(){return e},e}var H=z()($()),K=r("b2ee"),J=Object(n["defineComponent"])({name:"Registration",components:{AppCard:_["a"],Loader:T["a"]},props:{},setup:function(){var e=Object(E["d"])(),t=Object(K["a"])(),r=t.token,o=t.clearStorage,c="Create account",a=Object(n["ref"])(""),s=Object(n["ref"])(!1),i=Object(n["ref"])(""),l=Object(n["reactive"])({userName:"",password:"",role:"User"});Object(n["onMounted"])((function(){o()}));var d=Object(U["b"])(H),u=d.executeMutation,b=d.fetching,p=d.error,f=Object(n["computed"])((function(){return"Too weak"!==a.value})),g=Object(n["computed"])((function(){return""===l.password||l.password!==i.value})),m=Object(n["computed"])((function(){return""===l.userName||g.value||!f.value})),j=function(){var e=new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})"),t=new RegExp("^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})"),r=l.password;e.test(r)?a.value="Strong password":t.test(r)?a.value="Could be stronger":a.value="Too weak"},x=function(){var t=Object(I["a"])(regeneratorRuntime.mark((function t(){return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return t.next=2,u({input:l}).then((function(t){if(t.data&&t.data.registration.token)return r.value=t.data.registration.token,e.push(A["d"].Home),Promise.resolve()}));case 2:case"end":return t.stop()}}),t)})));return function(){return t.apply(this,arguments)}}();return{loadCreate:b,errCreate:p,registrationRequest:l,togglePassword:s,title:c,confirmPwd:i,checkPasswordStrength:j,passwordStrengthText:a,registrationDisabled:m,onRegistrationClick:x}}});r("5440");J.render=D,J.__scopeId="data-v-adadc650";t["default"]=J}}]);
//# sourceMappingURL=identity.2a56d4ab.js.map