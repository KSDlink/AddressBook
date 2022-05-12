<template>
  <form @submit.prevent>
    <h4>{{ title }}</h4>
    <div class="d-flex flex-column mb-3" v-if='v$.contactResault.$invalid'>
      <small v-if="v$.contactResault.email.$invalid" style="color: red">• Введите корректный адрес электронной почты</small>
      <small v-if="v$.contactResault.phoneNumber.$invalid" style="color: red">• Введите корректный номер телефона</small>
    </div>
    <div class="input-group mb-3">
      <custom-input type='text' class='form-control' v-model.trim='state.contactResault.firstName' placeholder='Имя'/>
    </div>
    <div class="input-group mb-3">
      <custom-input type='text' class='form-control' v-model.trim='state.contactResault.lastName' placeholder='Фамилия'/>
    </div>
    <div class="input-group mb-3">
      <span class="input-group-text">&#128231;</span>
      <custom-input type='email' class='form-control' v-model.trim='v$.contactResault.email.$model' placeholder='E-mail'/>
    </div>
    <div class="input-group mb-3">
      <span class="input-group-text">&#128241;</span>
      <custom-input type='text' class='form-control' v-model.trim='v$.contactResault.phoneNumber.$model' placeholder='Номер телефона'/>
    </div>
    <div class="input-group mb-3">
      <custom-select class='form-select' v-model='state.contactResault.gender' :options='genderOptions'></custom-select>
      <!-- <custom-input class='form-control' type='text' v-model.trim='contactResault.gender' placeholder='Пол'/> -->
    </div>
    <div class="input-group mb-3">
      <span class="input-group-text">&#127874;</span>
      <custom-input type='date' class='form-control' v-model.trim='state.contactResault.birthday' placeholder='Дата рождения'/>
    </div>
    <custom-button :disabled="v$.contactResault.$invalid" class="btn btn-primary" @click='clickButton'>{{ button }}</custom-button>
  </form>
</template>

<script lang="ts">
export default {
    name: 'address-form'
}
</script>

<script setup lang="ts">
import { ref, defineProps, defineEmits, reactive } from 'vue';
import useVuelidate from '@vuelidate/core';
import { required, email } from '@vuelidate/validators';
import { contactItem } from '@/types';
//import { contactItem } from '@/types';

const checkPhoneNumber = (phone: any) => {
  let regex = /^((\+7|7|8)+([0-9]){10})$/
  return regex.test(phone)
}
const props = defineProps({
  title: String,
  button: String,
  contact: {},
});
const state = reactive({
  contactResault: Object.assign({}, props.contact) as contactItem, //СПРОСИТЬ
});
//const contactResault = ref(Object.assign({}, props.contact)); //СПРОСИТЬ
const genderOptions = ref([
        {value: 'Мужской', name: 'Мужской'},
        {value: 'Женский', name: 'Женский'},
        {value: 'Другой', name: 'Другой'},
      ]);
const emit = defineEmits(['clickButton']);
const clickButton = () => {
      emit('clickButton',  state.contactResault);
    };
const rules = {
  contactResault: {
    email: {
      required, email 
    },
    phoneNumber: {
      required, checkPhoneNumber
    },
  },
}
const v$ = useVuelidate(rules, state);
</script>

<style scoped>
  /* form {
    display: flex;
    flex-direction: column;
  } */
</style>
