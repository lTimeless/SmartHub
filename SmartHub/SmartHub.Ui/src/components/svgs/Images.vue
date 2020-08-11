<template>
    <img :src="getImageUrls" :alt="src" :height="getHeight" :width="getWidth">
</template>

<script>
    export default {
        name: 'Images',
        props: {
            width: {
                type: Number,
                default: 40
            },
            src: {
                type: String,
                required: true
            }
        },

        computed: {
            getWidth() {
                return this.width;
            },
            getHeight() {
                return Math.ceil((this.getWidth / 48) * 48);
            },
            getImageUrls() {
                return this.src;
            }
        }
    };
</script>

<style scoped>

</style>
