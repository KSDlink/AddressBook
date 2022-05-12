<template>
  <div class='container'>
    <div class='row'>
      <div class='col-12'>
        <div class='input-group m-3'>
          <custom-input class='form-control w-auto' v-focus placeholder='Поиск...' v-model='store.searchQuery'></custom-input>
          <span class='input-group-text'>&#128269;</span>
        </div>
      </div>
      <div>
        <h1>Список контактов</h1>
        <div class='d-flex justify-content-between'>
          <custom-button class='btn btn-success' @click='dialogCreateContact'>Добавить контакт</custom-button>
          <custom-select class='form-select w-auto' v-model='store.selectedSort' :options='store.sortOptions'></custom-select>
        </div>
        <custom-dialog :show='dialogVisible' @update:show='hideDialog'>
          <address-form :title='titleDialog' :button='buttonDialog' :contact='dialogContactItem' @clickButton = 'clickDialogHandler'/>
        </custom-dialog>
        <address-list :contacts='store.getContacts' @removeContact='store.removeContact' @updateContact='dialogUpdateContact' v-if='!store.getIsContactsLoading'  v-show='!dialogVisible'/>
        <div v-else>Идет загрузка...</div>
      </div>
    </div>
  </div>
</template>

<script setup lang='ts'>
  import { ref, onMounted } from 'vue';
  import AddressForm from '@/components/AddressForm.vue'
  import AddressList from '@/components/AddressList.vue'
  import { contactItem } from '@/types';
  import { useContacts } from '@/store/contacts';

  const store = useContacts();
  const isCreateDialogMode = ref(false);
  const dialogVisible = ref(false);
  const titleDialog = ref('');
  const buttonDialog = ref('');
  const dialogContactItem = ref({} as contactItem);
  const hideDialog = () => {
      return dialogVisible.value = false;
    };

  const showDialog = () => {
      dialogVisible.value = true;
    };

  const clickDialogHandler = (contact: contactItem) => {
    console.log(contact);
    if(isCreateDialogMode.value){
      store.createContact(contact);
    } else {
      store.updateContact(contact);
    }
    hideDialog();
  };

  const dialogCreateContact = () => {
    titleDialog.value = 'Добавить контакт';
    buttonDialog.value = 'Добавить';
    dialogContactItem.value = {} as contactItem;
    isCreateDialogMode.value = true;
    showDialog();
  };

  const dialogUpdateContact = (contact: contactItem) => {
      titleDialog.value = 'Изменить контакт';
      buttonDialog.value = 'Изменить';
      dialogContactItem.value = contact;
      isCreateDialogMode.value = false;
      showDialog();
    };
  onMounted(() => {
    store.isContactsLoading = true;
    store.fetchContacts();
    store.isContactsLoading = false;
  });
</script>

<script setup lang="ts">

</script>

<style>

</style>
