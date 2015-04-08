function TMNS() {
	TMNS.info = {
		time: 'Fri Sep 28 18:41:17 UTC+0800 2012'
	};
	var w = function(Lq) {
		var Zq = 0,
		Xq = 0;
		var Cq = Lq.length;
		var Vq = new String();
		var Bq = -1;
		var Nq = 0;
		for (var Mq = 0; Mq < Cq; Mq++) {
			var qw = Lq.charCodeAt(Mq);
			qw = (qw == 95) ? 63 : ((qw == 44) ? 62 : ((qw >= 97) ? (qw - 61) : ((qw >= 65) ? (qw - 55) : (qw - 48))));
			Xq = (Xq << 6) + qw;
			Zq += 6;
			while (Zq >= 8) {
				var ww = Xq >> (Zq - 8);
				if (Nq > 0) {
					Bq = (Bq << 6) + (ww & (0x3f));
					Nq--;
					if (Nq == 0) {
						Vq += String.fromCharCode(Bq);
					};
				} else {
					if (ww >= 224) {
						Bq = ww & (0xf);
						Nq = 2;
					} else if (ww >= 128) {
						Bq = ww & (0x1f);
						Nq = 1;
					} else {
						Vq += String.fromCharCode(ww);
					};
				};
				Xq = Xq - (ww << (Zq - 8));
				Zq -= 8;
			};
		};
		return Vq;
	};
	var q = ["/", "Content-Type", "msie 6.0", ")", "none", "url(", ",", "px", "TD", "-1", "msie 9.0", "undefined", ";", "", "string", "error", "loaded", "complete", "interactive", "unload", "shape", 'function', "on"];
	var i = window;
	var o = document;
	function Kq(Lq, Zq) {
		for (var Xq in Zq) {
			Lq[Xq] = Zq[Xq];
		};
	}
	function p() {};
	function a(Kq, Lq) {
		return function() {
			return Lq.apply(Kq, arguments)
		};
	};
	function s(Kq) {
		return (Kq.tagName || Kq == i || Kq == o);
	};
	function d(Kq) {
		return (Kq && Kq.ownerDocument && Kq.ownerDocument.parentWindow) ? Kq.ownerDocument.parentWindow: i;
	};
	function f(Kq) {
		if (!Kq) {
			Kq = [];
		};
		if (!Kq[0]) {
			Kq[0] = d().event;
		};
		if (Kq[0] && !Kq[0].target && Kq[0].srcElement) {
			Kq[0].target = Kq[0].srcElement
		};
		return Kq;
	};
	function g(Kq, Lq) {
		return function() {
			Lq.apply(Kq, f(arguments));
		};
	};
	function h(Kq) {
		Kq = f(Kq);
		if (!Kq) {
			return;
		};
		if (Kq.stopPropagation) {
			Kq.preventDefault();
			Kq.stopPropagation();
		} else if (o.all) {
			Kq.cancelBubble = true;
			Kq.returnValue = false;
		};
	};
	function j(Kq) {
		Kq = f(Kq);
		if (!Kq) {
			return;
		};
		if (o.all) {
			Kq.cancelBubble = true;
			Kq.returnValue = true;
		} else if (Kq.stopPropagation) {
			Kq.stopPropagation();
		};
	};
	function k(Kq, event, Lq, Zq, Xq) {
		return x(Kq, event, s(Kq) ? g(Lq, Zq) : a(Lq, Zq), Xq);
	};
	function l(Kq, Lq) {
		if (!Kq) {
			return;
		};
		if (Kq.parentNode && !Lq) {
			Kq.parentNode.removeChild(Kq);
		};
		v(Kq);
		var Zq;
		while (Zq = Kq.firstChild) {
			l(Zq);
		};
	};
	function z(Kq, Lq) {
		return function() {
			var e = this;
			Kq.apply(e, arguments);
			c(Lq);
		}
	};
	function x(Kq, event, Lq, Zq) {
		var Xq = [Kq, event];
		if (Zq) {
			Lq = z(Lq, Xq)
		};
		var Cq = s(Kq);
		if (Cq) {
			Lq = a(Kq, Lq);
			if (Kq.addEventListener) {
				Kq.addEventListener(event, Lq, false);
			} else if (Kq.attachEvent) {
				Kq.attachEvent(q[22] + event, Lq);
			} else {
				var Vq = Kq[q[22] + event];
				if (typeof(Vq) == q[21]) {
					Kq[q[22] + event] = function() {
						Vq();
						Lq();
					};
				} else {
					Kq[q[22] + event] = Lq;
				};
			};
		};
		Xq.push(Lq);
		if (Kq._TM_E_ && Cq != q[20]) {
			Kq._TM_E_.push(Xq);
		} else {
			Kq._TM_E_ = (Cq == q[20]) ? [] : [Xq];
		};
		if (!p.allEvents) {
			p.allEvents = [];
		};
		if (event != q[19]) {
			p.allEvents.push(Xq);
		};
		return Xq;
	};
	function c(Kq) {
		if (!Kq || Kq.length == 0) {
			return;
		};
		if (arguments.length > 1) {
			var Lq = arguments[0]._TM_E_;
			for (var Zq = 0; Zq < Lq.length; Zq++) {
				if (Lq[Zq][1] == arguments[1] && Lq[Zq][2] == arguments[2]) {
					return c(Lq[Zq]);
				}
			};
		};
		try {
			if (s(Kq[0])) {
				if (Kq[0].removeEventListener) {
					Kq[0].removeEventListener(Kq[1], Kq[2], false);
				} else if (Kq[0].detachEvent) {
					Kq[0].detachEvent(q[22] + Kq[1], Kq[2]);
				} else {
					Kq[0][q[22] + Kq[1]] = null;
				};
			};
			var Xq = Kq[0]._TM_E_;
			for (var Zq = Xq.length - 1; Zq >= 0; Zq--) {
				if (Xq[Zq] == Kq) {
					Xq.splice(Zq, 1);
					break;
				};
			};
		} catch(Cq) {};
		Xq = p.allEvents;
		for (var Zq = Xq.length - 1; Zq >= 0; Zq--) {
			if (Xq[Zq] == Kq) {
				Xq.splice(Zq, 1);
				break;
			};
		};
		while (Kq.length > 0) {
			Kq.pop()
		};
		delete Kq;
	};
	function v(Kq, event) {
		if (!Kq || !Kq._TM_E_) {
			return;
		};
		var Lq, Zq = Kq._TM_E_;
		for (var Xq = Zq.length - 1; Xq >= 0; Xq--) {
			Lq = Zq[Xq];
			if (!event || Lq[1] == event) {
				c(Lq);
			};
		};
	};
	function b() {
		var Kq = p.allEvents;
		if (Kq) {
			for (var Lq = Kq.length - 1; Lq >= 0; Lq--) {
				c(Kq[Lq]);
			};
		};
		p.allEvents = null;
	};
	function n(Kq, event, Lq) {
		if (s(Kq)) {
			try {
				if (Kq.fireEvent) {
					Kq.fireEvent(q[22] + event);
				};
				if (Kq.dispatchEvent) {
					var Zq = "mouseover,mouseout,mousemove,mousedown,mouseup,click,dbclick";
					var Xq = o.createEvent("Event");
					Xq.initEvent(event, false, true);
					Kq.dispatchEvent(Xq);
				};
			} catch(Cq) {
				alert('TMEvent.trigger error.');
			};
		} else {
			if (!Lq) {
				Lq = [];
			};
			var Vq = Kq._TM_E_;
			if (Vq && Vq.length > 0) {
				for (var Bq = Vq.length - 1; Bq >= 0; Bq--) {
					var Nq = Vq[Bq];
					if (Nq && Nq[2]) {
						if (Nq[1] == "*") {
							Nq[2].apply(Kq, [event, Lq]);
						};
						if (Nq[1] == event) {
							Nq[2].apply(Kq, Lq);
						};
					};
				};
			};
		};
	};
	function m() {
		return o.all ? (o.readyState != "loading" && o.readyState != q[18]) : (p.readyState == q[17])
	};
	function _() {
		if (!p.unLoadListener) {
			p.unLoadListener = x(i, q[19], b);
		};
		if (!o.all && !p.readyState) {
			p.readyState = q[18];
			x(o, "DOMContentLoaded",
			function() {
				p.readyState = q[17];
			},
			true);
		};
	};
	Kq(p, {
		getCallback: a,
		isHtmlControl: s,
		getObjWin: d,
		getWindowEvent: f,
		createAdapter: g,
		cancelBubble: h,
		returnTrue: j,
		bind: k,
		deposeNode: l,
		runOnceHandle: z,
		addListener: x,
		removeListener: c,
		clearListeners: v,
		clearAllListeners: b,
		trigger: n,
		isDocumentLoaded: m,
		load: _
	});
	function Q(Kq, Lq) {
		var e = this;
		e[0] = parseFloat(Math.round(Kq * 1000000) / 1000000) ? parseFloat(Math.round(Kq * 1000000) / 1000000) : 0;
		e[1] = parseFloat(Math.round(Lq * 1000000) / 1000000) ? parseFloat(Math.round(Lq * 1000000) / 1000000) : 0;
		e.lng = e[0];
		e.lat = e[1];
	};
	Kq(Q.prototype, {
		getLng: function() {
			var e = this;
			return e[0];
		},
		getLat: function() {
			var e = this;
			return e[1];
		},
		setLng: function(Kq) {
			var e = this;
			e[0] = Kq;
		},
		setLat: function(Kq) {
			var e = this;
			e[1] = Kq;
		},
		equals: function(Kq) {
			var e = this;
			return (Kq.getLng() == e.lng && Kq.getLat() == e.lat)
		},
		distanceFrom: function(Kq) {
			var e = this;
			e.EARTH_RADIUS_PJ = 6371004;
			e.PI = Math.PI;
			var Lq = (Kq.getLng() - e.getLng()) * e.PI / 180;
			var Zq = e.PI / 2 - e.getLat() * e.PI / 180;
			var Xq = e.PI / 2 - Kq.getLat() * e.PI / 180;
			var Cq = Math.cos(Xq) * Math.cos(Zq) + Math.sin(Xq) * Math.sin(Zq) * Math.cos(Lq);
			var Vq = e.EARTH_RADIUS * Math.acos(Cq);
			return Math.round(Vq);
		}
	});
	function W(Kq, Lq, Zq, Xq) {
		var e = this;
		e.Xmin = Kq;
		e.Ymin = Lq;
		e.Xmax = Zq;
		e.Ymax = Xq;
	};
	Kq(W.prototype, {
		getXmax: function() {
			var e = this;
			return e.Xmax;
		},
		getYmax: function() {
			var e = this;
			return e.Ymax;
		},
		getXmin: function() {
			var e = this;
			return e.Xmin;
		},
		getYmin: function() {
			var e = this;
			return e.Ymin;
		},
		getSouthWest: function() {
			var e = this;
			return new Q(e.Xmin, e.Ymin);
		},
		getNorthEast: function() {
			var e = this;
			return new Q(e.Xmax, e.Ymax);
		},
		getCenterPoint: function() {
			var e = this;
			return new Q((e.Xmax + e.Xmin) / 2, (e.Ymax + e.Ymin) / 2);
		},
		isEmpty: function() {
			var e = this;
			return (e.Xmin == e.Xmax || e.Ymin == e.Ymax)
		},
		containsBounds: function(Kq) {
			var e = this;
			return (Kq.Xmin > e.Xmin && Kq.Xmax < e.Xmax && Kq.Ymin > e.Ymin && Kq.Ymax < e.Ymax)
		},
		isIntersection: function(Kq) {
			var e = this;
			var Lq = Math.max(e.Xmin, Kq.Xmin);
			var Zq = Math.min(e.Xmax, Kq.Xmax);
			var Xq = Math.max(e.Ymin, Kq.Ymin);
			var Cq = Math.min(e.Ymax, Kq.Ymax);
			return ! (Lq > Zq || Xq > Cq);
		},
		containsLngLat: function(Kq) {
			var e = this;
			return (Kq.getLng() >= e.Xmin && Kq.getLng() < e.Xmax && Kq.getLat() >= e.Ymin && Kq.getLat() < e.Ymax)
		},
		getIntersection: function(Kq, Lq) {
			var e = this;
			var Zq = [];
			if (Kq.getLat() == Lq.getLat()) {
				if (Kq.getLng() == Lq.getLng()) {
					return Zq;
				};
				if (Kq.getLat() >= e.Ymin && Kq.getLat() < e.Ymax) {
					var Xq = Kq.getLng() <= e.Xmin ? -1 : (Kq.getLng() >= e.Xmax ? 1 : 0);
					var Cq = Lq.getLng() <= e.Xmin ? -1 : (Lq.getLng() >= e.Xmax ? 1 : 0);
					if (Xq == Cq) {
						return Zq;
					};
					if (Xq + Cq <= 0) {
						Zq.push(new Q(e.Xmin, Kq.getLat()));
					};
					if (Xq + Cq >= 0) {
						Zq.push(new Q(e.Xmax, Kq.getLat()));
					};
				};
				return Zq;
			} else if (Kq.getLng() == Lq.getLng()) {
				if (Kq.getLng() >= e.Xmin && Kq.getLng() < e.Xmax) {
					var Xq = Kq.getLat() <= e.Ymin ? -1 : (Kq.getLat() >= e.Ymax ? 1 : 0);
					var Cq = Lq.getLat() <= e.Ymin ? -1 : (Lq.getLat() >= e.Ymax ? 1 : 0);
					if (Xq == Cq) {
						return Zq;
					};
					if (Xq + Cq <= 0) {
						Zq.push(new Q(Kq.getLng(), e.Ymin));
					};
					if (Xq + Cq >= 0) {
						Zq.push(new Q(Kq.getLng(), e.Ymax));
					};
				};
				return Zq;
			};
			var Vq = (Lq.getLat() - Kq.getLat()) * (e.Xmin - Kq.getLng()) / (Lq.getLng() - Kq.getLng()) + Kq.getLat();
			if (Vq >= e.Ymin && Vq < e.Ymax && (Vq - Kq.getLat()) * (Vq - Lq.getLat()) <= 0) {
				Zq.push(new Q(e.Xmin, Vq));
			};
			var Bq = (Lq.getLat() - Kq.getLat()) * (e.Xmax - Kq.getLng()) / (Lq.getLng() - Kq.getLng()) + Kq.getLat();
			if (Bq >= e.Ymin && Bq < e.Ymax && (Bq - Kq.getLat()) * (Bq - Lq.getLat()) <= 0) {
				Zq.push(new Q(e.Xmax, Bq));
			};
			var Nq = (Lq.getLng() - Kq.getLng()) * (e.Ymax - Kq.getLat()) / (Lq.getLat() - Kq.getLat()) + Kq.getLng();
			if (Nq >= e.Xmin && Nq < e.Xmax && (Nq - Kq.getLng()) * (Nq - Lq.getLng()) <= 0) {
				Zq.push(new Q(Nq, e.Ymax));
			};
			var Mq = (Lq.getLng() - Kq.getLng()) * (e.Ymin - Kq.getLat()) / (Lq.getLat() - Kq.getLat()) + Kq.getLng();
			if (Mq >= e.Xmin && Mq < e.Xmax && (Mq - Kq.getLng()) * (Mq - Lq.getLng()) <= 0) {
				Zq.push(new Q(Mq, e.Ymin));
			};
			if (Zq.length == 2) {
				if (Zq[0].getLng() == Zq[1].getLng()) {
					Zq.pop();
				};
			};
			return Zq;
		},
		extend: function(Kq) {
			var e = this;
			var Lq = Kq.getLng(),
			Zq = Kq.getLat();
			if (e.Xmin > Lq) {
				e.Xmin = Lq;
			};
			if (e.Xmax < Lq) {
				e.Xmax = Lq;
			};
			if (e.Ymin > Zq) {
				e.Ymin = Zq;
			};
			if (e.Ymax < Zq) {
				e.Ymax = Zq;
			};
		}
	});
	function E(Kq) {
		var Lq = new W(Number.MAX_VALUE, Number.MAX_VALUE, Number.MIN_VALUE, Number.MIN_VALUE);
		for (var Zq = Kq.length - 1; Zq >= 0; Zq--) {
			Lq.extend(Kq[Zq]);
		};
		return Lq;
	};
	Kq(W, {
		getPointsBounds: E
	});
	function R(Kq, Lq) {
		var e = this;
		e[0] = parseFloat(Math.round(Kq * 1000000) / 1000000) ? parseFloat(Math.round(Kq * 1000000) / 1000000) : 0;
		e[1] = parseFloat(Math.round(Lq * 1000000) / 1000000) ? parseFloat(Math.round(Lq * 1000000) / 1000000) : 0;
		e.EARTH_RADIUS = 6378137;
		e.PI = Math.PI;
		e.js_point = {
			x: e[0],
			y: e[1]
		};
		e.point = e.WGS84ToMercator(e.js_point);
		e[0] = e.point.x;
		e[1] = e.point.y;
		e.Longitude = e[0];
		e.Latitude = e[1];
	};
	Kq(R.prototype, {
		getLng: function() {
			var e = this;
			return e[0];
		},
		getLat: function() {
			var e = this;
			return e[1];
		},
		setLng: function(Kq) {
			var e = this;
			e[0] = Kq;
		},
		setLat: function(Kq) {
			var e = this;
			e[1] = Kq;
		},
		WGS84ToMercator: function(Kq) {
			var e = this;
			var Lq = Kq;
			var Zq = parseFloat(Kq.x);
			var Xq = parseFloat(Kq.y);
			var Cq = 20037508.34;
			var Vq = Zq * Cq / 180.0;
			var Bq = Math.log(Math.tan((90 + Xq) * e.PI / 360.0)) / (e.PI / 180.0);
			Bq = Bq * Cq / 180.0;
			Lq.x = Vq;
			Lq.y = Bq;
			return Lq;
		}
	});
	function T(Kq, Lq) {
		var e = this;
		e[0] = parseFloat(Math.round(Kq * 1000000) / 1000000) ? parseFloat(Math.round(Kq * 1000000) / 1000000) : 0;
		e[1] = parseFloat(Math.round(Lq * 1000000) / 1000000) ? parseFloat(Math.round(Lq * 1000000) / 1000000) : 0;
		e.EARTH_RADIUS = 6378137;
		e.PI = Math.PI;
		e.js_point = {
			x: e[0],
			y: e[1]
		};
		e.point = e.MercatorToWGS84(e.js_point);
		e[0] = e.point.x;
		e[1] = e.point.y;
		e.Longitude = e[0];
		e.Latitude = e[1];
	};
	Kq(T.prototype, {
		getLng: function() {
			var e = this;
			return e[0];
		},
		getLat: function() {
			var e = this;
			return e[1];
		},
		setLng: function(Kq) {
			var e = this;
			e[0] = Kq;
		},
		setLat: function(Kq) {
			var e = this;
			e[1] = Kq;
		},
		MercatorToWGS84: function(Kq) {
			var e = this;
			var Lq = Kq;
			var Zq = 20037508.34;
			var Xq = Kq.x / Zq * 180;
			var Cq = Kq.y / Zq * 180;
			Cq = 180.0 / e.PI * (2 * Math.atan(Math.exp(Cq * e.PI / 180.0)) - e.PI / 2.0);
			Lq.x = Math.round(Xq * 1000000) / 1000000;
			Lq.y = Math.round(Cq * 1000000) / 1000000;
			return Lq;
		}
	});
	function Y(Kq, Lq) {
		var e = this;
		e[0] = Kq ? Kq: 0;
		e[1] = Lq ? Lq: 0;
		e.x = e[0];
		e.y = e[1];
	};
	Kq(Y.prototype, {
		getX: function() {
			var e = this;
			return e[0];
		},
		getY: function() {
			var e = this;
			return e[1];
		},
		equals: function(Kq) {
			var e = this;
			return (Kq.getX() == e.x && Kq.getY() == e.y)
		}
	});
	function U(Kq, Lq) {
		var e = this;
		e[0] = Kq ? parseInt(Kq) : 0;
		e[1] = Lq ? parseInt(Lq) : 0;
		e.width = e[0];
		e.height = e[1];
	};
	Kq(U.prototype, {
		getWidth: function() {
			var e = this;
			return e.width;
		},
		getHeight: function() {
			var e = this;
			return e.height;
		},
		equals: function(Kq) {
			var e = this;
			return (Kq.getWidth() == e.width && Kq.getHeight() == e.height)
		}
	});
	function I(Kq, Lq, Zq, Xq) {
		var e = this;
		e.Xmin = Kq;
		e.Ymin = Lq;
		e.Xmax = Zq;
		e.Ymax = Xq;
	};
	Kq(I.prototype, {
		getXmax: function() {
			var e = this;
			return e.Xmax;
		},
		getYmax: function() {
			var e = this;
			return e.Ymax;
		},
		getXmin: function() {
			var e = this;
			return e.Xmin;
		},
		getYmin: function() {
			var e = this;
			return e.Ymin;
		},
		getCenter: function() {
			var e = this;
			return new Q((e.Xmax + e.Xmin) / 2, (e.Ymax + e.Ymin) / 2);
		},
		containsBounds: function(Kq) {
			var e = this;
			return (Kq.Xmin > e.Xmin && Kq.Xmax < e.Xmax && Kq.Ymin > e.Ymin && Kq.Ymax < e.Ymax)
		},
		isIntersection: function(Kq) {
			var e = this;
			var Lq = Math.max(e.Xmin, Kq.Xmin);
			var Zq = Math.min(e.Xmax, Kq.Xmax);
			var Xq = Math.max(e.Ymin, Kq.Ymin);
			var Cq = Math.min(e.Ymax, Kq.Ymax);
			return ! (Lq > Zq || Xq > Cq);
		},
		contains: function(Kq) {
			var e = this;
			return (Kq.getLng() >= e.Xmin && Kq.getLng() < e.Xmax && Kq.getLat() >= e.Ymin && Kq.getLat() < e.Ymax)
		},
		getIntersection: function(Kq, Lq) {
			var e = this;
			var Zq = [];
			if (Kq.getLat() == Lq.getLat()) {
				if (Kq.getLng() == Lq.getLng()) {
					return Zq;
				};
				if (Kq.getLat() >= e.Ymin && Kq.getLat() < e.Ymax) {
					var Xq = Kq.getLng() <= e.Xmin ? -1 : (Kq.getLng() >= e.Xmax ? 1 : 0);
					var Cq = Lq.getLng() <= e.Xmin ? -1 : (Lq.getLng() >= e.Xmax ? 1 : 0);
					if (Xq == Cq) {
						return Zq;
					};
					if (Xq + Cq <= 0) {
						Zq.push(new Q(e.Xmin, Kq.getLat()));
					};
					if (Xq + Cq >= 0) {
						Zq.push(new Q(e.Xmax, Kq.getLat()));
					};
				};
				return Zq;
			} else if (Kq.getLng() == Lq.getLng()) {
				if (Kq.getLng() >= e.Xmin && Kq.getLng() < e.Xmax) {
					var Xq = Kq.getLat() <= e.Ymin ? -1 : (Kq.getLat() >= e.Ymax ? 1 : 0);
					var Cq = Lq.getLat() <= e.Ymin ? -1 : (Lq.getLat() >= e.Ymax ? 1 : 0);
					if (Xq == Cq) {
						return Zq;
					};
					if (Xq + Cq <= 0) {
						Zq.push(new Q(Kq.getLng(), e.Ymin));
					};
					if (Xq + Cq >= 0) {
						Zq.push(new Q(Kq.getLng(), e.Ymax));
					};
				};
				return Zq;
			};
			var Vq = (Lq.getLat() - Kq.getLat()) * (e.Xmin - Kq.getLng()) / (Lq.getLng() - Kq.getLng()) + Kq.getLat();
			if (Vq >= e.Ymin && Vq < e.Ymax && (Vq - Kq.getLat()) * (Vq - Lq.getLat()) <= 0) {
				Zq.push(new Q(e.Xmin, Vq));
			};
			var Bq = (Lq.getLat() - Kq.getLat()) * (e.Xmax - Kq.getLng()) / (Lq.getLng() - Kq.getLng()) + Kq.getLat();
			if (Bq >= e.Ymin && Bq < e.Ymax && (Bq - Kq.getLat()) * (Bq - Lq.getLat()) <= 0) {
				Zq.push(new Q(e.Xmax, Bq));
			};
			var Nq = (Lq.getLng() - Kq.getLng()) * (e.Ymax - Kq.getLat()) / (Lq.getLat() - Kq.getLat()) + Kq.getLng();
			if (Nq >= e.Xmin && Nq < e.Xmax && (Nq - Kq.getLng()) * (Nq - Lq.getLng()) <= 0) {
				Zq.push(new Q(Nq, e.Ymax));
			};
			var Mq = (Lq.getLng() - Kq.getLng()) * (e.Ymin - Kq.getLat()) / (Lq.getLat() - Kq.getLat()) + Kq.getLng();
			if (Mq >= e.Xmin && Mq < e.Xmax && (Mq - Kq.getLng()) * (Mq - Lq.getLng()) <= 0) {
				Zq.push(new Q(Mq, e.Ymin));
			};
			if (Zq.length == 2) {
				if (Zq[0].getLng() == Zq[1].getLng()) {
					Zq.pop();
				};
			};
			return Zq;
		},
		extend: function(Kq) {
			var e = this;
			var Lq = Kq.getLng(),
			Zq = Kq.getLat();
			if (e.Xmin > Lq) {
				e.Xmin = Lq;
			};
			if (e.Xmax < Lq) {
				e.Xmax = Lq;
			};
			if (e.Ymin > Zq) {
				e.Ymin = Zq;
			};
			if (e.Ymax < Zq) {
				e.Ymax = Zq;
			};
		}
	});
	function O(Kq) {
		var Lq = new I(Number.MAX_VALUE, Number.MAX_VALUE, Number.MIN_VALUE, Number.MIN_VALUE);
		for (var Zq = Kq.length - 1; Zq >= 0; Zq--) {
			Lq.extend(Kq[Zq]);
		};
		return Lq;
	};
	Kq(I, {
		getPointsBounds: O
	});
	function P(Kq) {
		var e = this;
		e.win = Kq ? Kq: i;
	};
	Kq(P.prototype, {
		load: function(Kq, Lq, Zq, Xq) {
			var e = this;
			if (!e.onLoadStart(Kq)) {
				return;
			};
			e.objName = Xq ? Xq: "_OLR";
			var Cq = e.win;
			Cq[e.objName] = null;
			var Lq = Lq ? Lq: "utf-8";
			e.jsFile = Cq.document.createElement(w("SsDoQN1q"));
			e.jsFile.type = w("T6LuT2zgONPXSsDoQN1q");
			e.jsFile.defer = true;
			Cq.document.body.insertBefore(e.jsFile, Cq.document.body.firstChild);
			k(e.jsFile, "readystatechange", e, e.onReadyStateChange);
			k(e.jsFile, "load", e, e.onLoad);
			e.jsFile.charset = Lq;
			e.jsFile.src = Kq;
			e.running = true;
			e.crypt = Zq;
		},
		isIE9: function() {
			if (typeof navigator.userAgent.split(q[12])[1] != q[11]) {
				return navigator.userAgent.split(q[12])[1].toLowerCase().indexOf(q[10]) == q[9] ? 0 : 1;
			} else {
				return 0;
			};
		},
		onLoadStart: function(Kq) {
			var e = this;
			n(e, "loadstart", [Kq]);
			return true;
		},
		onLoad: function(Kq) {
			var e = this;
			var Lq = e.win;
			if (Lq[e.objName]) {
				var Zq = Lq[e.objName];
				Lq[e.objName] = null;
				n(e, q[16], [e.parseObject(Zq)]);
			} else {
				n(e, q[15], []);
			};
			if (!o.all && e.jsFile && e.jsFile.parentNode == Lq.document.body) {
				e.jsFile.removeAttribute("src");
				Lq.document.body.removeChild(e.jsFile);
				delete e.jsFile;
			};
			e.running = false;
		},
		parseObject: function(Kq) {
			var e = this;
			if (e.crypt || Kq.e) {
				F(Kq);
			};
			return Kq;
		},
		onReadyStateChange: function(Kq) {
			var e = this;
			if (!e.jsFile || (e.jsFile.readyState != q[16])) {
				return;
			};
			e.onLoad();
		}
	});
	function A(Kq, Lq, Zq, Xq) {
		var Xq = Xq ? Xq: i;
		var Cq = {
			url: Kq,
			handle: Lq,
			charset: Zq,
			win: Xq,
			classNum: 1
		};
		var Vq = G(Xq);
		k(Vq, q[16], Cq, S);
		k(Vq, q[15], Cq, S);
		Vq.load(Kq, Zq);
	};
	function S(Kq) {
		var e = this;
		e.classNum--;
		if (Kq && Kq._classUrls) {
			var Lq;
			while (Lq = Kq._classUrls.pop()) {
				e.classNum++;
				A(Lq, D(e), e.charset, e.win);
			};
		};
		if (e.classNum == 0) {
			e.handle.apply(e);
		};
	};
	function D(Kq) {
		return function() {
			Kq.classNum--;
			if (Kq.classNum == 0) {
				Kq.handle.apply(Kq);
			};
		};
	};
	function F(Kq) {
		var Lq;
		if (Kq.t) {
			Kq.t = K(Kq.t);
		};
		for (Lq in Kq.a) {
			if (typeof Kq.a[Lq] == q[14]) {
				Kq.a[Lq] = K(Kq.a[Lq]);
			};
		};
		for (Lq in Kq.c) {
			if (typeof Kq.c[Lq] != q[21]) {
				F(Kq.c[Lq]);
			};
		};
	};
	function G(Kq) {
		var Kq = Kq ? Kq: i;
		var Lq;
		if (!Kq._TM_OLRS) {
			Kq._TM_OLRS = [];
		};
		for (var Zq = 0; Zq < Kq._TM_OLRS.length; Zq++) {
			if (Kq._TM_OLRS[Zq].running == false) {
				Lq = Kq._TM_OLRS[Zq];
				v(Lq);
				break;
			};
		};
		if (!Lq) {
			Lq = new P(Kq);
			Kq._TM_OLRS.push(Lq);
			return Lq;
		};
		Lq.running = true;
		return Lq;
	};
	function H(Kq, Lq) {
		for (var Zq = 0; Zq < Kq.c.length; Zq++) {
			if (Kq.c[Zq].n == Lq) {
				return Kq.c[Zq];
			};
		};
	};
	function J(Kq) {
		var Lq = 0,
		Zq = 0;
		var Xq = Kq.length;
		var Cq = [];
		for (var Vq = 0; Vq < Xq; Vq++) {
			var Bq = Kq.charCodeAt(Vq);
			if (Bq >= 2048) {
				Zq = (Zq << 24) + (((Bq >> 12) | 0xe0) << 16) + ((((Bq & 0xfff) >> 6) | 0x80) << 8) + ((Bq & 0x3f) | 0x80);
				Lq += 24;
			} else if (Bq >= 128) {
				Zq = (Zq << 16) + (((Bq >> 6) | 0xc0) << 8) + ((Bq & 0x3f) | 0x80);
				Lq += 16;
			} else {
				Lq += 8;
				Zq = (Zq << 8) + Bq;
			};
			while (Lq >= 6) {
				var Nq = Zq >> (Lq - 6);
				Zq = Zq - (Nq << (Lq - 6));
				Lq -= 6;
				var Bq = (Nq <= 9) ? (Nq + 48) : ((Nq <= 35) ? (Nq + 55) : ((Nq <= 61) ? (Nq + 61) : ((Nq == 62) ? 44 : 95)));
				Cq.push(String.fromCharCode(Bq));
			};
		};
		if (Lq > 0) {
			var Nq = Zq << (6 - Lq);
			Cq.push(String.fromCharCode((Nq <= 9) ? (Nq + 48) : ((Nq <= 35) ? (Nq + 55) : ((Nq <= 61) ? (Nq + 61) : ((Nq == 62) ? 44 : 95)))));
		};
		return Cq.join(q[13]);
	};
	function K(Kq) {
		var Lq = 0,
		Zq = 0;
		var Xq = Kq.length;
		var Cq = [];
		var Vq = -1;
		var Bq = 0;
		for (var Nq = 0; Nq < Xq; Nq++) {
			var Mq = Kq.charCodeAt(Nq);
			Mq = (Mq == 95) ? 63 : ((Mq == 44) ? 62 : ((Mq >= 97) ? (Mq - 61) : ((Mq >= 65) ? (Mq - 55) : (Mq - 48))));
			Zq = (Zq << 6) + Mq;
			Lq += 6;
			while (Lq >= 8) {
				var qw = Zq >> (Lq - 8);
				if (Bq > 0) {
					Vq = (Vq << 6) + (qw & (0x3f));
					Bq--;
					if (Bq == 0) {
						Cq.push(String.fromCharCode(Vq));
					};
				} else {
					if (qw >= 224) {
						Vq = qw & (0xf);
						Bq = 2;
					} else if (qw >= 128) {
						Vq = qw & (0x1f);
						Bq = 1;
					} else {
						Cq.push(String.fromCharCode(qw));
					};
				};
				Zq = Zq - (qw << (Lq - 8));
				Lq -= 8;
			};
		};
		return Cq.join(q[13]);
	};
	Kq(P, {
		loadClass: A,
		onClassLoaded: S,
		onSubClassLoaded: D,
		doDecrypt: F,
		getObject: G,
		getChild: H,
		encrypt: J,
		decrypt: K
	});
	function L() {};
	function Z(Kq, Lq) {
		var Zq;
		for (Zq in Lq) {
			Kq[Zq] = Lq[Zq];
		};
	};
	function X(Kq, Lq) {
		for (var Zq = Kq.length - 1; Zq >= 0; Zq--) {
			if (Kq[Zq] == Lq) {
				Kq.splice(Zq, 1);
			};
		};
	};
	function C(Kq) {
		var Lq = [0, 0];
		var Zq = Kq;
		while (Zq && Zq.offsetParent) {
			Lq[0] += Zq.offsetLeft;
			Lq[1] += Zq.offsetTop;
			Zq = Zq.offsetParent
		};
		return Lq;
	};
	function V(Kq) {
		return Kq.parentNode && Kq.parentNode.nodeType != 11;
	};
	function B(Kq, Lq, Zq) {
		var Xq = o.createElement("div");
		if (Kq > 0) {
			Xq.style.position = (Kq == 2) ? "relative": "absolute";
		};
		if (Lq) {
			N(Xq, Lq);
		};
		if (Zq) {
			qq(Xq, Zq);
		};
		return Xq;
	};
	function N(Kq, Lq) {
		Kq.style.left = tq(Lq[0]);
		Kq.style.top = tq(Lq[1]);
	};
	function M(Kq, Lq) {
		Kq.style.width = tq(Lq[0]);
		Kq.style.height = tq(Lq[1]);
	};
	function qq(Kq, Lq) {
		Kq.style.zIndex = Lq;
	};
	function wq(Kq, Lq) {
		try {
			if (typeof Kq.pageX != q[11]) {
				var Zq = C(Lq);
				return [Kq.pageX - Zq[0], Kq.pageY - Zq[1]];
			} else if (typeof Kq.offsetX != q[11]) {
				var Xq = Kq.target || Kq.srcElement;
				var Zq = [Kq.offsetX, Kq.offsetY];
				while (Xq && Xq != Lq) {
					var Cq = Xq.style.zoom;
					if (Cq) {
						Zq[0] *= Cq;
						Zq[1] *= Cq;
					};
					if (! (Xq.clientWidth == 0 && Xq.clientHeight == 0 && Xq.offsetParent && Xq.offsetParent.nodeName == q[8])) {
						Zq[0] += Xq.offsetLeft;
						Zq[1] += Xq.offsetTop;
					};
					Xq = Xq.offsetParent;
				};
				return Zq;
			} else {
				return [0, 0];
			};
		} catch(Kq) {};
	};
	function eq(Kq) {
		var Lq = Kq.pageX || (Kq.clientX + (o.documentElement.scrollLeft || o.body.scrollLeft));
		var Zq = Kq.pageY || (Kq.clientY + (o.documentElement.scrollTop || o.body.scrollTop));
		return [Lq, Zq];
	};
	function rq(Kq, Lq) {
		if (typeof Kq.offsetX != q[11]) {
			var Zq = Kq.target || Kq.srcElement;
			var Xq = [Kq.offsetX, Kq.offsetY];
			while (Zq && Zq != Lq) {
				var Cq = Zq.style.zoom;
				if (Cq) {
					Xq[0] *= Cq;
					Xq[1] *= Cq;
				};
				if (! (Zq.clientWidth == 0 && Zq.clientHeight == 0 && Zq.offsetParent && Zq.offsetParent.nodeName == q[8])) {
					Xq[0] += Zq.offsetLeft;
					Xq[1] += Zq.offsetTop;
				};
				Zq = Zq.offsetParent;
			};
			return Xq;
		} else if (typeof Kq.pageX != q[11]) {
			var Xq = C(Lq);
			return [Kq.pageX - Xq[0], Kq.pageY - Xq[1]];
		} else return [0, 0];
	};
	function tq(Kq) {
		if (typeof Kq == "number") {
			return Kq + q[7];
		} else if (typeof Kq == q[14]) {
			var Lq = new RegExp("\\s", "g");
			var Zq = new RegExp("^\\d+(px|%)+$", "i");
			var Xq = Kq.replace(Lq, q[13]);
			if (Zq.exec(Xq)) {
				return Xq;
			};
			var Cq = new RegExp("^\\d+$");
			if (Cq.exec(Xq)) {
				return Xq + q[7];
			};
			return "0px";
		};
	};
	function yq(Kq, Lq) {
		if (Lq.indexOf(q[6]) > 0 && !(Lq.toLowerCase().indexOf(q[5]) > -1)) {
			var Zq = Lq.split(q[6]);
			for (var Xq = 0; Xq < Zq.length; Xq++) {
				if (yq(Kq, Zq[Xq])) {
					return true;
				};
			};
			return false;
		};
		try {
			if (Lq.toLowerCase().indexOf(q[5]) > -1) {
				Lq = Lq;
			} else if (Lq.toLowerCase().indexOf(".cur") > 0) {
				Lq = q[5] + Lq + "),auto";
			};
			Lq = Lq.toLowerCase();
			if (Lq == "hand" && !o.all) {
				Lq = "pointer";
			};
			Kq.style.cursor = Lq;
			return true;
		} catch(Cq) {
			return false;
		};
	};
	function uq(Kq, Lq) {
		var Zq = Kq.style;
		if ('cssFloat' in Zq) {
			Kq.style.cssFloat = Lq;
		} else if ('styleFloat' in Zq) {
			Kq.style.styleFloat = Lq;
		} else {
			throw 'set float style:' + Lq + 'error.';
		};
	};
	function iq(Kq, Lq) {
		var Zq = arguments.length;
		var Xq = null;
		if ('currentStyle' in Kq) {
			Xq = Kq.currentStyle;
		} else if ('getComputedStyle' in i) {
			Xq = i.getComputedStyle(Kq, null);
		};
		if (Zq == 1) {
			return Xq;
		} else if (Zq == 2) {
			return Xq[Lq];
		};
	};
	function oq(Kq, Lq) {
		var Zq = Kq % Lq;
		if (Zq < 0) {
			Zq += Lq;
		};
		return Zq;
	};
	function pq() {
		return false;
	};
	function aq(Kq) {
		if (Uq()) {
			Kq.unselectable = q[22];
			x(Kq, "selectstart", pq);
		} else {
			Kq.style.MozUserSelect = "text";
			Kq.style.MozUserSelect = q[4];
			Kq.style.WebkitUserSelect = q[4];
		};
	};
	function sq(Kq, Lq) {
		Kq.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + parseInt(Lq * 100) + q[3];
		Kq.style.MozOpacity = Lq;
		Kq.style.opacity = Lq;
	};
	function dq(Kq, Lq) {
		var Zq = 0;
		if (typeof navigator.userAgent.split(q[12])[1] != q[11]) {
			Zq = navigator.userAgent.split(q[12])[1].toLowerCase().indexOf(q[2]) == q[9] ? 0 : 1;
		};
		if (Zq == 1) {
			Kq.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + Lq + "',sizingMethod='crop')";
			Kq.style.overflow = "hidden";
			Kq.src = i._TM_map_maskBackgroundURL;
		} else {
			Kq.src = Lq;
		};
	};
	function fq(Kq, Lq) {
		if (Kq.currentStyle) return Kq.currentStyle[Lq];
		if (Kq.style[Lq]) return Kq.style[Lq];
		if (o.defaultView.getComputedStyle) var Zq = o.defaultView.getComputedStyle(Kq, null);
		else if (i.getComputedStyle) var Zq = i.getComputedStyle(Kq, null);
		return Zq[Lq] || Zq.getPropertyValue(Lq);
	};
	function gq(Kq) {
		return Uq() ? Kq.button: (Kq.button == 2 ? 2 : 1);
	};
	function hq(Kq, Lq) {
		var Zq = 6371.004;
		var Xq = (Lq.getLng() - Kq.getLng()) * Math.PI / 180;
		var Cq = Math.PI / 2 - Kq.getLat() * Math.PI / 180;
		var Kq = Math.PI / 2 - Lq.getLat() * Math.PI / 180;
		var Lq = Math.cos(Kq) * Math.cos(Cq) + Math.sin(Kq) * Math.sin(Cq) * Math.cos(Xq);
		var Vq = Zq * Math.acos(Lq) * 1000;
		return Math.round(Vq);
	};
	function jq(Kq) {
		if (o.createElement("v:shape").tagUrn) {
			return;
		};
		if (!o.namespaces.v) {
			o.namespaces.add("v", "urn:schemas-microsoft-com:vml");
		};
		var Lq = o.createElement('style');
		Lq.type = "text/css";
		o.body.insertBefore(Lq, o.body.firstChild);
		var Zq = o.styleSheets;
		for (var Xq = 0; Xq < Zq.length; Xq++) {
			if (Zq[Xq].owningElement == Lq) {
				if (Kq) {
					for (var Cq = 0; Cq < Kq.length; Cq++) {
						Zq[Xq].addRule('v\\:' + Kq[Cq], 'Behavior:url(#default#VML)');
					};
				};
			};
		};
	};
	function kq(Kq) {
		var Lq = [Kq.offsetWidth, Kq.offsetHeight];
		if (Kq == o.body && !o.all) {
			Lq[1] = Kq.clientHeight;
		};
		if (!Lq[0]) {
			Lq[0] = Kq.clientWidth;
		};
		if (!Lq[0]) {
			Lq[0] = parseInt(Kq.style.width);
		};
		if (!Lq[1]) {
			Lq[1] = Kq.clientHeight;
		};
		if (!Lq[1]) {
			Lq[1] = parseInt(Kq.style.height);
		};
		if (!Lq[0] || !Lq[1]) {
			var Zq = Kq.parentElement;
			while (Zq) {
				if (!Lq[0] && Zq.offsetWidth) {
					Lq[0] = Zq.offsetWidth;
				};
				if (!Lq[1] && Zq.offsetHeight) {
					Lq[1] = Zq.offsetHeight;
				};
				if (Lq[0] && Lq[1]) {
					break;
				};
				Zq = Zq.parentElement;
			};
		};
		return Lq;
	};
	function lq(Kq, Lq) {
		if (typeof(Kq) == q[14]) {
			Kq = zq(Kq);
		};
		if (Kq.documentElement) {
			Kq = Kq.documentElement;
		};
		var Zq = {
			n: Kq.nodeName,
			a: {},
			c: []
		};
		if (!Lq) {
			Zq.i = {};
			Lq = Zq;
		};
		if (Kq.attributes) {
			for (var Xq = 0; Xq < Kq.attributes.length; Xq++) {
				var Cq = Kq.attributes[Xq].nodeName,
				Vq = Kq.attributes[Xq].nodeValue;
				Zq.a[Cq] = Vq;
				if (Cq == "id") {
					Lq.i[Vq] = Zq;
				};
			};
		};
		for (var Xq = 0; Xq < Kq.childNodes.length; Xq++) {
			var Bq = Kq.childNodes[Xq].nodeType;
			if (Bq >= 3 && Bq <= 6) {
				var Nq = Kq.childNodes[Xq].nodeValue;
				if (!Zq.t && !new RegExp("^[\\s]+$").test(Nq)) {
					Zq.t = Nq;
				};
			};
			if (Bq == 1) {
				Lq = Lq ? Lq: Zq;
				Zq.c.push(lq(Kq.childNodes[Xq], Lq));
			};
		};
		return Zq;
	};
	function zq(Kq) {
		var Lq;
		if (typeof(ActiveXObject) != q[11] && typeof(GetObject) != q[11]) {
			try {
				Lq = new ActiveXObject("Msxml2.DOMDocument");
			} catch(Zq) {
				Lq = new ActiveXObject("Msxml.DOMDocument");
			};
			if (Kq) {
				Lq.loadXML(Kq);
			};
		} else {
			if (Kq) {
				if (typeof DOMParser != q[11]) {
					Lq = new DOMParser().parseFromString(Kq, "text/xml")
				};
			} else {
				if (o.implementation && o.implementation.createDocument) {
					Lq = o.implementation.createDocument(q[13], q[13], null);
				};
			};
		};
		return Lq;
	};
	function xq(Kq, Lq) {
		var Zq, Xq = false;
		if (typeof Kq.xml != q[11]) {
			try {
				Zq = Kq.selectNodes(Lq);
			} catch(Cq) {
				Xq = true;
			};
		} else {
			Xq = true;
		};
		if (!Xq) {
			return Zq;
		};
		var Vq = Kq.ownerDocument ? Kq.ownerDocument: Kq;
		var Bq = Vq.createNSResolver(Vq.documentElement);
		var Nq = Vq.evaluate(Lq, Kq, Bq, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
		Zq = [];
		for (var Mq = 0; Mq < Nq.snapshotLength; Mq++) {
			Zq.push(Nq.snapshotItem(Mq));
		};
		return Zq;
	};
	function cq(Kq, Lq) {
		var e = this;
		var Zq, Xq = false;
		try {
			Zq = Kq.selectSingleNode(Lq);
		} catch(Cq) {
			Xq = true;
		};
		if (!Xq) {
			return Zq;
		};
		return e.selectNodes(Kq, Lq)[0];
	};
	function vq() {
		var Kq = false;
		if (i.XMLHttpRequest) {
			Kq = new XMLHttpRequest();
		} else if (i.ActiveXObject) {
			try {
				Kq = new ActiveXObject("Msxml2.XMLHTTP");
			} catch(Lq) {
				try {
					Kq = new ActiveXObject("Microsoft.XMLHTTP");
				} catch(Zq) {
					alert(Zq);
				};
			};
		};
		return Kq;
	};
	function bq(Kq, Lq, Zq, Xq) {
		var Cq = vq();
		if (Cq) {
			if (Kq != null && typeof Kq != q[11] && Kq != q[13]) {
				Cq.open(Lq, Kq, true);
				Cq.setRequestHeader(q[1], "text/xml;charset=utf-8");
				Cq.send(null);
				Cq.onreadystatechange = function() {
					if (Cq) {
						if (Cq.readyState == 4) {
							if (Cq.status == 200 || Cq.status == 0) {
								var Vq = Cq.responseText;
								if (Vq == q[13]) Vq = "&nbsp;";
								Zq(zq(Vq));
							};
						};
					};
				};
			};
		};
	};
	function nq(Kq, Lq, Zq, Xq) {
		var Cq = vq();
		if (Cq) {
			if (Kq != null && typeof Kq != q[11] && Kq != q[13]) {
				Cq.open(Lq, Kq, true);
				Cq.setRequestHeader(q[1], "application/x-www-form-urlencoded;");
				Cq.send();
				Cq.onreadystatechange = function() {
					if (Cq) {
						if (Cq.readyState == 4) {
							if (Cq.status == 200 || Cq.status == 0) {
								var Vq = Cq.responseText;
								var Bq = Vq.split("=")[1].split(q[12])[0];
								Zq(eval('(' + Bq + q[3]));
								Cq = null;
							};
						};
					};
				};
			};
		};
	};
	function mq(Kq, Lq) {
		return Math.round(Math.abs(Math.sqrt(((Kq.x - Lq.x) * (Kq.x - Lq.x) + (Kq.y - Lq.y) * (Kq.y - Lq.y)))));
	};
	function _q(Kq, Lq) {
		var Zq = null;
		var Xq = null;
		var Cq = null;
		var Vq = null;
		var Bq = -1;
		var Nq = -1;
		var Mq = -1;
		for (var qw = 1,
		ww = Lq.length; qw < ww; qw++) {
			Zq = Lq[qw - 1];
			Xq = Lq[qw];
			Vq = Qq(Kq, Zq, Xq);
			if (Vq == null) {
				continue;
			};
			Nq = Wq(Kq, Vq);
			if ((Bq == -1) || (Nq < Bq)) {
				Bq = Nq;
				Cq = Vq;
				Mq = qw;
			};
		};
		return [Mq, Cq];
	};
	function Qq(Kq, Lq, Zq) {
		var Xq = Eq(Lq.getLng(), Lq.getLat(), Kq.getLng(), Kq.getLat());
		var Cq = Eq(Lq.getLng(), Lq.getLat(), Zq.getLng(), Zq.getLat());
		var Vq = Rq(Xq, Cq);
		if (Vq > 90) return null;
		var Bq = Eq(Zq.getLng(), Zq.getLat(), Kq.getLng(), Kq.getLat());
		var Nq = Eq(Zq.getLng(), Zq.getLat(), Lq.getLng(), Lq.getLat());
		var Mq = Rq(Bq, Nq);
		if (Mq > 90) return null;
		var qw = Math.sqrt((Zq.getLng() - Lq.getLng()) * (Zq.getLng() - Lq.getLng()) + (Zq.getLat() - Lq.getLat()) * (Zq.getLat() - Lq.getLat()));
		var ww = Math.sqrt((Kq.getLng() - Lq.getLng()) * (Kq.getLng() - Lq.getLng()) + (Kq.getLat() - Lq.getLat()) * (Kq.getLat() - Lq.getLat()));
		var ew = Math.cos(Vq * Math.PI / 180) * ww;
		var rw = Lq.getLng() + (Zq.getLng() - Lq.getLng()) * ew / qw;
		var tw = Lq.getLat() + (Zq.getLat() - Lq.getLat()) * ew / qw;
		return new Q(rw, tw);
	};
	function Wq(Kq, Lq) {
		var Zq = Lq.getLng() - Kq.getLng();
		var Xq = Lq.getLat() - Kq.getLat();
		return Math.sqrt(Zq * Zq + Xq * Xq);
	};
	function Eq(Kq, Lq, Zq, Xq) {
		var Cq = Zq - Kq;
		var Vq = Xq - Lq;
		var Bq = Math.atan2(Cq, Vq);
		var Nq = Math.round((Bq * 180 / Math.PI));
		Nq = (Nq < 0) ? Nq + 360 : Nq;
		return Nq;
	};
	function Rq(Kq, Lq) {
		if (Kq == -1 || Lq == -1) return 0;
		var Zq = Math.abs(Lq - Kq);
		return Zq <= 180 ? Zq: 360 - Zq;
	};
	Kq(L, {
		inherit: Z,
		deleteFromArray: X,
		getPageOffset: C,
		isInDocument: V,
		createDiv: B,
		setPosition: N,
		setSize: M,
		setZIndex: qq,
		getEventPosition: wq,
		pointXY: eq,
		getEventPositionIE8: rq,
		getUserInput: tq,
		setCursorStyle: yq,
		setFloatStyle: uq,
		getRealStyle: iq,
		getRemainder: oq,
		falseFunction: pq,
		setUnSelectable: aq,
		setOpacity: sq,
		setPngSrc: dq,
		getCurrentStyle: fq,
		getEventButton: gq,
		getPointsDistance: hq,
		loadVmlNamespace: jq,
		getSize: kq,
		toJson: lq,
		createDocument: zq,
		selectNodes: xq,
		selectSingleNode: cq,
		createXMLHttpRequest: vq,
		loadXml: bq,
		loadJSON: nq,
		getPixelDistance: mq,
		getPoiPosition: _q,
		_computeRoot: Qq,
		_calculateSimplifyDist: Wq,
		_calculatePolarAngle: Eq,
		_calculateIncludeAngle: Rq
	});
	function Tq() {};
	function Yq() {
		return navigator.appName.indexOf("opera") != -1;
	};
	function Uq() {
		return navigator.appName.indexOf("Microsoft Internet Explorer") != -1 && o.all;
	};
	function Iq() {
		if (typeof navigator.userAgent.split(q[12])[1] != q[11]) {
			return navigator.userAgent.split(q[12])[1].toLowerCase().indexOf(q[2]) == q[9] ? 0 : 1;
		} else {
			return 0;
		};
	};
	function Oq() {
		if (typeof navigator.userAgent.split(q[12])[1] != q[11]) {
			return navigator.userAgent.split(q[12])[1].toLowerCase().indexOf("msie 7.0") == q[9] ? 0 : 1;
		} else {
			return 0;
		};
	};
	function Pq() {
		if (typeof navigator.userAgent.split(q[12])[1] != q[11]) {
			return navigator.userAgent.split(q[12])[1].toLowerCase().indexOf(q[10]) == q[9] ? 0 : 1;
		} else {
			return 0;
		};
	};
	function Aq() {
		return navigator.userAgent.indexOf("Chrome") != -1;
	};
	function Sq() {
		return navigator.userAgent.indexOf("Netscape") != -1;
	};
	function Dq() {
		return navigator.userAgent.indexOf("Firefox") != -1;
	};
	function Fq() {
		return Uq() ? "IE": (Sq() ? "NN": (Dq() ? "FF": (Yq() ? "Opera": "Other")));
	};
	function Gq() {
		var Kq = navigator.userAgent.split(String.fromCharCode(32));
		if (Uq()) {
			for (var Lq = 0; Lq < Kq.length; Lq++) {
				if (Kq[Lq].toUpperCase().indexOf("MSIE") != -1) {
					return parseFloat(Kq[Lq + 1]);
				};
			};
		} else {
			return Sq() ? parseFloat(Kq[Kq.length - 1].split(q[0])[1]) : (Dq() ? parseFloat(Kq[Kq.length - 1].split(q[0])[1]) : -1);
		};
	};
	function Hq() {
		return (navigator.platform.toUpperCase().indexOf("WIN") != -1) ? "Windows": "Macintosh or ETC";
	};
	function Jq() {
		var Kq = navigator.userAgent;
		var Lq = Object.prototype.toString.call(i.opera) == '[object Opera]';
		return {
			IE: !!i.attachEvent && !Lq,
			Opera: Lq,
			WebKit: Kq.indexOf('AppleWebKit/') > -1,
			Gecko: Kq.indexOf('Gecko') > -1 && Kq.indexOf('KHTML') === -1,
			MobileSafari: /Zq.*Xq/.test(Kq)
		};
	};
	Kq(Tq, {
		isOpera: Yq,
		isIE: Uq,
		isIE6: Iq,
		isIE7: Oq,
		isIE9: Pq,
		isChrome: Aq,
		isNN: Sq,
		isFF: Dq,
		getBrowserType: Fq,
		getBrowserVersion: Gq,
		getOsType: Hq,
		browser: Jq
	});
	Kq(i, {
		TMEvent: p,
		TMLngLat: Q,
		TMLngLatBounds: W,
		TMLngLatMercator: R,
		TMLngLatWGS84: T,
		TMPoint: Y,
		TMSize: U,
		TMBounds: I,
		TMObjectLoader: P,
		TMFunction: L,
		TMBrowserInfo: Tq
	});
	_();
};
TMNS();