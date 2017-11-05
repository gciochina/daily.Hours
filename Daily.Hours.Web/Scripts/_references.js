﻿/// <reference path="jquery-1.12.4.js" />
/// <autosync enabled="true" />
/// <reference path="../_spa/_references.js" />
/// <reference path="../_spa/components/mainView.js" />
/// <reference path="../_spa/components/topBarView.js" />
/// <reference path="../_spa/js/site.js" />
/// <reference path="../_spa/lib/bootcards/dist/js/bootcards.js" />
/// <reference path="../_spa/lib/bootcards/gruntfile.js" />
/// <reference path="../_spa/lib/bootcards/src/js/bootcards.js" />
/// <reference path="../_spa/lib/bootstrap/dist/js/bootstrap.js" />
/// <reference path="../_spa/lib/bootstrap/dist/js/npm.js" />
/// <reference path="../_spa/lib/fastclick/lib/fastclick.js" />
/// <reference path="../_spa/lib/jquery/dist/jquery.js" />
/// <reference path="../_spa/lib/jquery-validation/dist/additional-methods.js" />
/// <reference path="../_spa/lib/jquery-validation/dist/jquery.validate.js" />
/// <reference path="../_spa/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js" />
/// <reference path="../_spa/lib/knockout/build/fragments/amd-post.js" />
/// <reference path="../_spa/lib/knockout/build/fragments/amd-pre.js" />
/// <reference path="../_spa/lib/knockout/build/fragments/extern-post.js" />
/// <reference path="../_spa/lib/knockout/build/fragments/extern-pre.js" />
/// <reference path="../_spa/lib/knockout/build/fragments/source-references.js" />
/// <reference path="../_spa/lib/knockout/build/knockout-raw.js" />
/// <reference path="../_spa/lib/knockout/dist/knockout.js" />
/// <reference path="../_spa/lib/knockout/gruntfile.js" />
/// <reference path="../_spa/lib/knockout/src/binding/bindingattributesyntax.js" />
/// <reference path="../_spa/lib/knockout/src/binding/bindingprovider.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/attr.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/checked.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/click.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/css.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/enabledisable.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/event.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/foreach.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/hasfocus.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/html.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/ififnotwith.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/options.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/selectedoptions.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/style.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/submit.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/text.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/textinput.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/uniquename.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/value.js" />
/// <reference path="../_spa/lib/knockout/src/binding/defaultbindings/visible.js" />
/// <reference path="../_spa/lib/knockout/src/binding/editdetection/arraytodomnodechildren.js" />
/// <reference path="../_spa/lib/knockout/src/binding/editdetection/comparearrays.js" />
/// <reference path="../_spa/lib/knockout/src/binding/expressionrewriting.js" />
/// <reference path="../_spa/lib/knockout/src/binding/selectextensions.js" />
/// <reference path="../_spa/lib/knockout/src/components/componentbinding.js" />
/// <reference path="../_spa/lib/knockout/src/components/customelements.js" />
/// <reference path="../_spa/lib/knockout/src/components/defaultloader.js" />
/// <reference path="../_spa/lib/knockout/src/components/loaderregistry.js" />
/// <reference path="../_spa/lib/knockout/src/google-closure-compiler-utils.js" />
/// <reference path="../_spa/lib/knockout/src/memoization.js" />
/// <reference path="../_spa/lib/knockout/src/namespace.js" />
/// <reference path="../_spa/lib/knockout/src/options.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/dependencydetection.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/dependentobservable.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/extenders.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/mappinghelpers.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/observable.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/observableArray.changeTracking.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/observablearray.js" />
/// <reference path="../_spa/lib/knockout/src/subscribables/subscribable.js" />
/// <reference path="../_spa/lib/knockout/src/tasks.js" />
/// <reference path="../_spa/lib/knockout/src/templating/jquery.tmpl/jquerytmpltemplateengine.js" />
/// <reference path="../_spa/lib/knockout/src/templating/native/nativetemplateengine.js" />
/// <reference path="../_spa/lib/knockout/src/templating/templateengine.js" />
/// <reference path="../_spa/lib/knockout/src/templating/templaterewriting.js" />
/// <reference path="../_spa/lib/knockout/src/templating/templatesources.js" />
/// <reference path="../_spa/lib/knockout/src/templating/templating.js" />
/// <reference path="../_spa/lib/knockout/src/utils.domData.js" />
/// <reference path="../_spa/lib/knockout/src/utils.domManipulation.js" />
/// <reference path="../_spa/lib/knockout/src/utils.domNodeDisposal.js" />
/// <reference path="../_spa/lib/knockout/src/utils.js" />
/// <reference path="../_spa/lib/knockout/src/version.js" />
/// <reference path="../_spa/lib/knockout/src/virtualelements.js" />
/// <reference path="../_spa/lib/requirejs-plugins/examples/img/relativepath.js" />
/// <reference path="../_spa/lib/requirejs-plugins/lib/markdown.converter.js" />
/// <reference path="../_spa/lib/requirejs-plugins/lib/require.js" />
/// <reference path="../_spa/lib/requirejs-plugins/lib/text.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/async.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/depend.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/font.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/goog.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/image.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/json.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/mdown.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/noext.js" />
/// <reference path="../_spa/lib/requirejs-plugins/src/propertyparser.js" />
/// <reference path="../_spa/scripts/init.js" />
/// <reference path="../bower_components/jquery/dist/jquery.js" />
/// <reference path="../bower_components/knockout/dist/knockout.js" />
/// <reference path="../bower_components/requirejs-plugins/lib/Markdown.Converter.js" />
/// <reference path="../bower_components/requirejs-plugins/lib/text.js" />
/// <reference path="../bower_components/requirejs-plugins/src/async.js" />
/// <reference path="../bower_components/requirejs-plugins/src/depend.js" />
/// <reference path="../bower_components/requirejs-plugins/src/font.js" />
/// <reference path="../bower_components/requirejs-plugins/src/goog.js" />
/// <reference path="../bower_components/requirejs-plugins/src/image.js" />
/// <reference path="../bower_components/requirejs-plugins/src/json.js" />
/// <reference path="../bower_components/requirejs-plugins/src/mdown.js" />
/// <reference path="../bower_components/requirejs-plugins/src/noext.js" />
/// <reference path="../bower_components/requirejs-plugins/src/propertyParser.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />