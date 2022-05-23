Разное
Подключение Vue
Привязки

    //============================ Разное ============================
    // index.html
    < !DOCTYPE html >
<html>
    <head>
        <title>Super Title</title>        
        <link rel="stylesheet" href="css/main.css">
    </head>
    <body>
        <div id="app">
            <span v-on:click="title='New Text'">Button 1</span>     <!-- v- говорит, что дальше идет код VueJs -->
                                                                    <!-- При клике на 'text1', переменная title изменится -->
            <span @click="title='New Text'">Button 2</span>         <!-- 'v-on:' можно заменить на '@' -->
            <span @mouseover="title='New Text'">Button 3</span>     <!-- Обработка наведения курсора на текст -->
            <span @mouseover="changeText">Button 4</span>           <!-- Вызов метода -->
            <p>{{title}}</p>
            <p v-bind:class="styleCss">Цвет: {{styleCss}}</p>       <!-- 'v-bind:' привызявает значение переменной -->
            <p :clas="styleCss"> Цвет: {{styleCss}}</p>             <!--Можно писать просто ':' -- >
            < !--$event.target.value вернет значение, которое мы введем в input-- >
    <input type="text" v-on:input="styleCss=$event.target.value">
        </div>
    </br >
        <div id="app2">
            <input type="text" v-on:input="increment($event.target.value)">
            <p>{{value}}</p>
            <p>{{doubleValue}}</p>
        </div>
    </br>
        <div id="app3">
            <button v-on:click="show = !show">Show / Hide</button>
            <p v-if="show">Some text</p>        <!-- 'v-if' удаляет объект из HTML -->
                                                <!-- 'v-show' скрывает объект в HTML -->
            <p v-else="show">Some text 2</p>    <!-- Если в условии выше 'false', то отработает этот блок -->
        </div>
    </br >
        <div id="app4">
            <ul>
                <li>{{cars[2].model}}</li>      <!-- Обращаемся к элементу массива -->

                <!-- В цикле for перебираем все элементы массива -->
                <!-- i - это индекс, можно не указывать -->
                <li v-for="(car, i) in cars">{{i}}: {{car.model}}, скорость {{car.speed}}</li>
            </ul>

            <!--Фильтры -->
            <p v-else>{{showMess}}</p>

            <!-- '|' означает, что мы к message можем применить наш фильтр lowercase -->
            <!-- lowercase - это фильтр, которы мы написали и который преобразует все в нижний формат -->
            <!-- Фильтров может быть сколько угодно -->
            <p>{{message | lowercase | filter1}}</p>

            <!-- Вызов нашего компонента -->
            <comp1></comp1>
            <comp2></comp2>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/vue"></script>
        <script src="vue/vue.js"></script>
        <script src="./script.js"></script>
    </body >
</html >

//----------------------------------------------------------------
// main.css
.grey {
    background: grey;
}

.yellow {
    background: yellow;
}

.pink {
    background: pink;
}

.blue {
    background: blue;
}

.car {
    width: 30 %;
    margin: 10px 31 %;
    background: #fafafa;
    border: 2px solid silver;
    border - radius: 10px;
    padding: 1.2 %, 2 %;
    color: #474747;
    font - size: 1.1em;
}

//----------------------------------------------------------------
// Создание глобального фильтра, который делает первый символ каждого слова в верхнем регистре
// Глобальный фильтр можно использовать в любом объекте, а не только там, где он определен
Vue.filter('filter1', function (value) {
    if (!value)
        return ''
    value = value.toString();
    return value.replace(/\b\w/g, function (l) {
        return l.toUpperCase();
    });
});
new Vue({
    el: '#app',              // Найти в HTML элемент с id=app
    data: {
        title: "Old Text",
        styleCss: ''
    },
    methods: {
        changeText() {
            this.title = "Some new text";
        }
    }
});
new Vue({
    el: '#app2',
    data: {
        value: 1
    },
    methods: {
        increment(data) {
            this.value = data;
            if (data == 25) {
                alert("Number 25");
            }
        }
    },
    computed: {
        doubleValue() {
            return this.value * 2;
        }
    }
});
new Vue({
    el: '#app3',
    data: {
        show: false // Если false, то текст будет скрыт
    }
});
// Глобальный компонент (ф-я)
// Должен быть определен до использования
Vue.component('comp1', {
    data: function () {
        return {
            cars: [
                { model: "Model 1", speed: 1 },
                { model: "Model 2", speed: 2 },
                { model: "Model 3", speed: 3 }
            ]
        };
    },
    // Код ниже должен быть в одну строку и содержать один родительский объект, здесь это div
    template: '<div><div class="car" v-for="car in cars"><p>{{car.model}}</p></div></div>'
});

new Vue({
    el: "#app4",
    data: {
        message: 'HELLO',
        cars: [
            { model: "Model 1", speed: 1 },
            { model: "Model 2", speed: 2 },
            { model: "Model 3", speed: 3 }
        ]
    },
    computed: {
        showMess() {
            return this.message.toUpperCase();
        }
    },
    filters: {
        lowercase(value) {
            return value.toLowerCase();
        }
    },
    // Локальный компонент
    components: {
        'comp2': {
            data: function () {
                return {
                    cars: [
                        { model: "Model 1", speed: 1 },
                        { model: "Model 2", speed: 2 },
                        { model: "Model 3", speed: 3 }
                    ]
                };
            },
            template: '<div><div class="car" v-for="car in cars"><p>{{car.model}}</p></div></div>'
        }
    }
});

//----------------------------- App.vue --------------------------
<template>
  <div id="app">
    <product-list-one></product-list-one>
    <product-list-two></product-list-two>
  </div>
</template>

<script>
import ProductListOne from './components/ProductListOne.vue';
import ProductListTwo from './components/ProductListTwo.vue';

export default {
  components: {
    'product-list-one': ProductListOne,
    'product-list-two': ProductListTwo
  },
  name: 'app',
  data () {
    return {
      
    }
  }
}
</script>
<style>
</style>

//----------------------------- main.js --------------------------
import Vue from 'vue'
import App from './App.vue'
import { store } from './store/store'

new Vue({
    store: store,
    el: '#app',
    render: h => h(App)
})

    //------------------------ ProductListOne.vue --------------------
    < template >
    <div id="product-list-one">
        <h2>Product List One</h2>
        <ul>
            <li v-for="product in products">
                <span class="name">{{ product.name }}</span>
                <span class="name">{{ product.price }}</span>
            </li>
        </ul>
    </div>
</template >

<script>
export default {
    computed: {
        products() {
            return this.$store.state.products
        }
    }
}
</script>
<style>
</style>

//------------------------ ProductListTwo.vue --------------------
<template>
  <div id="product-list-two">
    <h2>Product List Two</h2>
    <ul>
        <li v-for="product in products">
            <span class="name">{{product.name}}</span>
            <span class="name">{{product.price}}</span>
        </li>
    </ul>
  </div>
</template>

<script>
export default {
    computed: {
        products() {
            return this.$store.state.products
        }
    }
}
</script>
<style>
</style>

//---------------------------- store.js --------------------------
import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        products: [
            { name: 'Name 1', price: 1 },
            { name: 'Name 2', price: 2 },
            { name: 'Name 3', price: 3 },
            { name: 'Name 4', price: 4 }
        ]
    }
});

//======================== Подключение Vue =======================
// Подключение через ссылку
<body>
    <p>Текст</p>
    <script src="https://cdn.jsdelivr.net/npm/vue"></script>
    <script type="text/javascript">
        var sample = new Vue({
            el: document.getElementById('app')
        });
    </script>
</body>

// Через HTML
<body>
    <div id="app">
        <p>Тект</p>            
    </div>
    <sctipt type="text/javascript">
        var sample = new Vue({
            el: document.getElementById('app')  // Привязка через HTML
        });
    </sctipt>
</body>

// Через Css
<head>   
    <script src="vue.js"></script>
    <script type="text/javascript">
    window.onload = function() {
        var sample = new Vue({
            el: "#app",                         // Привязка через css
            data: { x: 10 }
        });
    }
    </script>
</head>
<body>
    <div id="app">
        <p>Тект</p>
        {{x}}
    </div>
</body>

//============================ Привязки===========================
Привязка - это подключение бизнес - логики к HTML - странице
    < html >
    <head>
        <script src="vue.js"></script>
        <script type="text/javascript">
            window.onload = function() {
                var sample = new Vue({
                    el: "#app",
                    data: {
                        name: 'Anna',
                        path: '<a href="https://google.com">Google</a>',
                        firstStyle: {
                            color: 'blue',
                            fontFamily: 'Verdana',
                            textAlign: 'left',
                            border: '2px solid red'
                        },
                        secondStyle: {
                            color: 'green',
                            fontFamily: 'Verdana',
                            textAlign: 'left',
                            border: '2px solid orange'
                        }
                    }
                });
            }
        </script>
    </head>
    <body>
        <div id="app">
            <p>{{name}}</p>                                 <!-- Два способа получения значения переменной -->
            <p v-text="name"></p>
            <p v-once="name"></p>                           <!-- v-once отображает значение элемента только один раз -->
            <p v-once="name"></p>                           <!-- Второй раз элемент не отобразится -->
            <p>{{path}}</p>                                 <!-- Вернет <a href="https://google.com">Google</a> -->
            <p v-html="path"></p>                           <!-- Вернет ссылку -->
            <p v-text='name' v-bind:style="firstStyle"></p> <!-- v-bind принимает значение аттрибута -->

            <!-- Привязка нескольких стилей. Если есть повторяющиеся элементы (color), то применится значение последнего -->
            <p v-text='name' v-bind:style="[firstStyle, secondStyle]"></p>  
            <p v-text='name' :style="firstStyle"></p>       <!-- Сокращенный вариант -->
        </div>
    </body >
</html >